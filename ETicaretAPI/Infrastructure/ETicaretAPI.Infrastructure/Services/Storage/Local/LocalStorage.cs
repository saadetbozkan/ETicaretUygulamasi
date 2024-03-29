﻿
using ETicaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        readonly IWebHostEnvironment webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
             => File.Delete($"{path}\\{fileName}");
    
        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

        async Task<bool> CopyFilesAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }

        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(this.webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string filename, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName, HasFile);
                await CopyFilesAsync(Path.Combine(uploadPath, fileNewName), file);
                datas.Add((fileNewName, Path.Combine(path, fileNewName)));
            }
           
            return datas;
        }
    }
}
