using System;
using ExifLib;
using System.Linq;
using System.IO;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReadImageExif
{
    class Program
    {
        const string SETTINGS_FILE = "./settings.json";
        const string LAST_RUN_FILE = "./lastrun.json";
        const string DEFAULT_SOURCE_PATH = @"C:\Users\coats\OneDrive\SkyDrive camera roll";
        const string DEFAULT_OUTPUT_PATH = @"C:\Temp";
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        string databaseName = "ImageData";
        string containerName = "ExifData";

        static async Task Main(string[] args)
        {
            // private string cosmosConnectionId;
            var config = new ConfigurationBuilder()
                            .AddUserSecrets<Program>()
                            .Build();
            var cosmosConnectionString = config["Cosmos:ConnectionString"];


            Settings settings;
            if (File.Exists(SETTINGS_FILE))
            {
                settings = JsonConvert.DeserializeObject<Settings>(await File.ReadAllTextAsync(SETTINGS_FILE));
            }
            else
            {
                settings = new Settings() { SourcePath = DEFAULT_SOURCE_PATH, OutputPath = DEFAULT_OUTPUT_PATH, Locations = new Location[] { } };
                await File.WriteAllTextAsync(SETTINGS_FILE, JsonConvert.SerializeObject(settings));
            }

            DateTime lastRun;
            if (File.Exists(LAST_RUN_FILE))
            {
                lastRun = JsonConvert.DeserializeObject<DateTime>(await File.ReadAllTextAsync(LAST_RUN_FILE));
            }
            else
            {
                lastRun = DateTime.MinValue;
            }

            var timeNow = DateTime.UtcNow;


            var di = new DirectoryInfo(settings.SourcePath);

            var p = new Program();


            p.CreateCosmosClient(cosmosConnectionString);
            await p.CreateDatabaseAsync();
            await p.CreateContainerAsync();

            // only jpgs taken in the last day
            // the commented out lines were used for testing, but are left them in there for reference
            foreach (var file in di.EnumerateFileSystemInfos()
                                .Where(f => f.Extension == ".jpg" && f.CreationTimeUtc >= lastRun)
                                // .OrderByDescending(f => f.CreationTimeUtc)
                                // .Skip(1000)
                                // .Take(10)
                                )
            {
                var fileData = new FileData();
                fileData.FilePath = di.FullName;
                fileData.FileName = file.FullName;
                fileData.ExifData = file.FullName.GetExifData();

                await p.AddItemToContainerAsync(fileData);
            }


            await File.WriteAllTextAsync(LAST_RUN_FILE, JsonConvert.SerializeObject(DateTime.UtcNow));

            foreach (var loc in settings.Locations.Where(l => l.Process))
            {
                await p.GetMatchingFiles(loc, lastRun, settings.OutputPath);
                // await p.GetMatchingFiles(loc, DateTime.MinValue, settings.OutputPath);
            }

            await p.GetPhotoLocationsAsText(settings.OutputPath);
            

        }

        private async Task GetPhotoLocationsAsText(string outputPath)
        {
            var locs = new {
                type = "Feature Collection", 
                features = container.GetItemLinqQueryable<FileData>(allowSynchronousQueryExecution: true)
                    .Where(fd=> fd.ExifData.Location != null 
                        )
                    .Select(f=> new {type = "Feature", id = f.Id,  properties= new {fileName = f.FileName},  geometry = new {type="Point", coordinates= new Double[] {f.ExifData.GPSLongitudeDecimal.Value, f.ExifData.GPSLatitudeDecimal.Value, 0d}}}),
                bbox = new Double[] {
                    -122.52323605555556,
                    -45.048291666666664,
                    0d,
                    174.76611111111112,
                    60.706586111111115,
                    0d
                }
                };
            
            await File.WriteAllTextAsync(Path.Combine(outputPath, "HeatMapTest.json"), JsonConvert.SerializeObject(locs));
            using (var file = File.CreateText(Path.Combine(outputPath, "HeatMapTest.csv")))
            {
                foreach (var feat in locs.features)
                {
                    await file.WriteLineAsync($"{feat.geometry.coordinates[0]},{feat.geometry.coordinates[1]}");
                }
            }
        }

        private async Task GetMatchingFiles(Location loc, DateTime lastRun, string outputPath)
        {
            foreach (var fd in container.GetItemLinqQueryable<FileData>(allowSynchronousQueryExecution: true)
                .Where(
                    f => f.ExifData.DateTimeDigitized >= lastRun
                    && f.ExifData.Location.Distance(loc.Coordinates) <= loc.Threshold
                    && f.ExifData.AspectRatioString == "landscape"
                )
            )
            {
                if (File.Exists(fd.FileName))
                {
                    var diLoc = Directory.CreateDirectory(Path.Combine(outputPath, loc.Name));
                    if (!File.Exists(Path.Combine(diLoc.FullName, Path.GetFileName(fd.FileName))))
                    {
                        File.Copy(fd.FileName, Path.Combine(diLoc.FullName, Path.GetFileName(fd.FileName)), true);
                    }
                }
            }
        }

        private void CreateCosmosClient(string connectionString)
        {
            cosmosClient = new CosmosClient(connectionString);

        }

        private async Task CreateDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            System.Console.WriteLine($"Created Database: {this.database.Id}");
        }

        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerName, "/FilePath");
            Console.WriteLine($"Created Container: {this.container.Id}");
        }

        private async Task AddItemToContainerAsync(FileData item)
        {
            ItemResponse<FileData> itemResponse = await this.container.UpsertItemAsync(item, new PartitionKey(item.FilePath));
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", itemResponse.Resource.Id, itemResponse.RequestCharge);

        }
    }


}
