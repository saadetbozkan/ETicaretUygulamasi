using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETicaretAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient blobServiceClient;
        BlobContainerClient blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            this.blobServiceClient = new(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = this.blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
           return  this.blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            return this.blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            await this.blobContainerClient.CreateIfNotExistsAsync();
            await this.blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            List <(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(containerName, file.Name, HasFile);
                BlobClient blobClient = this.blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, containerName));
            }
            return datas;
        }
    }
}
