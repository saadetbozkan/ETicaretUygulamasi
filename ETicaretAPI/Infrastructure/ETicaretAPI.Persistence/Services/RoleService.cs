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

        public async Task<bool> DeleteRoleAsync(string id)
        {
            AppRole role = await this.roleManager.FindByIdAsync(id);
            IdentityResult result = await this.roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public (int, Dictionary<string, string>) GetAllRole(int page, int size)
        {
            // size veya page -1 olması halinde bütün roles verileri döner.

            var query = this.roleManager.Roles;

            IQueryable<AppRole> rolesQuery = null;
            if (page != -1 && size != -1)
                rolesQuery = query.Skip(page * size).Take(size);
            else
                rolesQuery = query;
            
            return (query.Count(), rolesQuery.ToDictionary(role => role.Id, role => role.Name));
        }

        public async Task<(string id, string name)> GetRoleByIdAsync(string id)
        {
            string role = await this.roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            AppRole role = await this.roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await this.roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
