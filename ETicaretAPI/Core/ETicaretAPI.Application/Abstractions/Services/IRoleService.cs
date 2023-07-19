namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        (int, Dictionary<string, string>) GetAllRole(int page, int size);
        Task<(string id, string name)> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string name);
        Task<bool> UpdateRoleAsync(string id, string name);

    }
}
