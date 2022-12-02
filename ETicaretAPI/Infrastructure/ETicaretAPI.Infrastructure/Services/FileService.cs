using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFilesAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
            
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            
        }

        public async Task<List<(string filename, string path)>> UploadAsync(string path, IFormFileCollection files)
        {

            string uploadPath = Path.Combine(this.webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string filename, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);
                bool result = await CopyFilesAsync(Path.Combine(uploadPath, fileNewName), file);
                datas.Add((fileNewName, Path.Combine(uploadPath, fileNewName)));
            }
            if(results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            return null;
            //todo Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata aldığına dair uyarıcı bir exception oluşturulup fırlatılması gerekiyor!
        }
    } 
}
