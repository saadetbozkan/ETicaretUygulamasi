using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Persistence.Services
{
    public class RoleService: IRoleService
    { 
        readonly RoleManager<AppRole> roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await this.roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;    
        }

        public async Task<bool> DeleteRoleAsync(string name)
        {
            IdentityResult result = await this.roleManager.DeleteAsync(new() { Name = name });
            return result.Succeeded;
        }

        public (int, Dictionary<string, string>) GetAllRole(int page, int size)
        {

            var Alldatas = this.roleManager.Roles.ToList();

            int totalRolesCount = Alldatas.Count;
            
            var datas = Alldatas.Skip(page * size).Take(size).ToDictionary(role => role.Id, role => role.Name);

            return (totalRolesCount, datas);
        }

        public async Task<(string id, string name)> GetRoleByIdAsync(string id)
        {
            string role = await this.roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            IdentityResult result = await this.roleManager.UpdateAsync(new() { Id = id, Name = name });
            return result.Succeeded;
        }
    }
}
