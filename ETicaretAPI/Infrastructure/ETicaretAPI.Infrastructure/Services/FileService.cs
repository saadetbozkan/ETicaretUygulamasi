
using ETicaretAPI.Infrastructure.Operations;
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
    public class FileService
    {
        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true, int count = 2)
        {
            return await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;

                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                }
                else
                    newFileName = fileName;

                if (File.Exists($"{path}\\{newFileName}"))
                    return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName).Split("-")[0]}-{count}{extension}", false, ++count);
                else
                    return newFileName;

            });
            
        }

    } 
}
