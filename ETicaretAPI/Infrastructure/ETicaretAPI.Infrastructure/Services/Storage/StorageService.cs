using ETicaretAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage storage;

        public StorageService(IStorage storage)
        {
            this.storage = storage;
        }

        public string StorageName => this.storage.GetType().Name;

        public async Task DeleteAsync(string pathOrContainer, string fileName)
            => await this.storage.DeleteAsync(pathOrContainer, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => this.storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => this.storage.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => this.storage.UploadAsync(pathOrContainerName, files);
    }
}
