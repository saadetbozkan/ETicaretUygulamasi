using ETicaretAPI.Infrastructure.Operations;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainer, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainer, string fileName, HasFile hasFileMethod, bool first = true, int count = 2)
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

                if (hasFileMethod(pathOrContainer, newFileName))
                    return await FileRenameAsync(pathOrContainer, $"{Path.GetFileNameWithoutExtension(newFileName).Split("-")[0]}-{count}{extension}", hasFileMethod, false, ++count);
                else
                    return newFileName;

            });

        }
    }
}