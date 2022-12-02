using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string filename, string path)>> UploadAsync(string path, IFormFileCollection files);
        Task<string> FileRenameAsync(string fileName);

        Task<bool> CopyFilesAsync(string path, IFormFile file);

    }
}
