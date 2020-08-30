using System;
using ExifLib;
using System.Linq;
using System.IO;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ReadImageExif
{
    class Program
    {
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
            Console.WriteLine("Hello World!");
            var cosmosConnectionString = config["Cosmos:ConnectionString"];
            System.Console.WriteLine(cosmosConnectionString);

            var path = @"C:\Users\coats\OneDrive\SkyDrive camera roll";
            var di = new DirectoryInfo(path);

            var p = new Program();


            p.CreateCosmosClient(cosmosConnectionString);
            await p.CreateDatabaseAsync();
            await p.CreateContainerAsync();


            foreach (var file in di.EnumerateFileSystemInfos()
                                .Where(f => f.Extension == ".jpg")
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
