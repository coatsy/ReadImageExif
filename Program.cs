﻿using System;
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
                settings = new Settings() { SourcePath = DEFAULT_SOURCE_PATH, OutputPath = DEFAULT_OUTPUT_PATH, LastRun = DateTime.MinValue, Locations = new Location[] { } };
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
                                .Where(f => f.Extension == ".jpg" && f.CreationTimeUtc >= settings.LastRun)
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

            settings.LastRun = timeNow;

            foreach (var loc in settings.Locations.Where(l => l.Process))
            {
                // await p.GetMatchingFiles(loc, settings.LastRun, settings.OutputPath);
                await p.GetMatchingFiles(loc, DateTime.MinValue, settings.OutputPath);
            }

            await File.WriteAllTextAsync(SETTINGS_FILE, JsonConvert.SerializeObject(settings));



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
