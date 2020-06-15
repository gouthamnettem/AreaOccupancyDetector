using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MockDataGenerator.ServiceHelper
{
    public class Generator : IGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly string storageConnectionString;
        public Generator(IConfiguration configuration)
        {
            _configuration = configuration;
            storageConnectionString = _configuration.GetValue<string>("storageConnectionString");
        }
        public void GenerateData()
        {
            List<JObject> mockEntities = new List<JObject>();
            string mockDataString = GetMockDataFile();
            mockEntities = JsonConvert.DeserializeObject<List<JObject>>(mockDataString);
            DateTime timeStamp = DateTime.Now;
            foreach (JObject mockEntity in mockEntities)
            {
                mockEntity["TimeStamp"] = timeStamp;
                mockEntity["Employee Count"] = Count();
            }
            byte[] byteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(mockEntities).ToString());
            MemoryStream stream = new MemoryStream(byteArray);
            UploadFile(stream);
        }
        public int Count()
        {
            Random random = new Random();
            return random.Next(_configuration.GetValue<int>("minRangeValue"), _configuration.GetValue<int>("maxRangeValue"));
        }
        public void UploadFile(Stream stream)
        {
            var date = DateTime.Now;
            string fileName = string.Format("{0}-{1}-{2}-{3}{4}{5}.json", date.Day, date.Month, date.Year, date.Hour, date.Minute, date.Millisecond);
            // Initialise client in a different place if you like

            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = account.CreateCloudBlobClient();

            // Make sure container is there
            var blobContainer = blobClient.GetContainerReference(_configuration.GetValue<string>("blobContainer"));
            blobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = "application/json";
            blockBlob.UploadFromStreamAsync(stream);
        }
        public string GetMockDataFile()
        {
            // Setup the connection to the storage account
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = account.CreateCloudBlobClient();

            // Make sure container is there
            var blobContainer = blobClient.GetContainerReference(_configuration.GetValue<string>("inputFileContainer"));
            blobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(_configuration.GetValue<string>("mockDataFile"));
            // Get the blob file as text
            string contents = blockBlob.DownloadTextAsync().Result;

            return contents;
        }
    }
}
