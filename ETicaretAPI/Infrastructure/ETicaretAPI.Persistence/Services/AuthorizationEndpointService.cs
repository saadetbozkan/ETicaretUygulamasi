using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService applicationService;
        readonly IEndpointReadRepository endpointReadRepository;
        readonly IEndpointWriteRepository endpointWriteRepository;
        readonly IMenuWriteRepository menuWriteRepository;
        readonly IMenuReadRepository menuReadRepository;
        readonly RoleManager<AppRole> roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuWriteRepository menuWriteRepository, IMenuReadRepository menuReadRepository, RoleManager<AppRole> roleManager)
        {
            this.applicationService = applicationService;
            this.endpointReadRepository = endpointReadRepository;
            this.endpointWriteRepository = endpointWriteRepository;
            this.menuWriteRepository = menuWriteRepository;
            this.menuReadRepository = menuReadRepository;
            this.roleManager = roleManager;
        }

        public async Task AuthorizationEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await this.menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await this.menuWriteRepository.AddAsync(_menu);

                await this.menuWriteRepository.SaveAsync();
            }
               

            Endpoint? endpoint = await this.endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.AppRoles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu );

            if (endpoint == null)
            {
                var action = this.applicationService.GetAuthorizeDefinitionEndpoints(type)
                    .FirstOrDefault(m => m.Name == menu)?
                    .Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu
                };

                await this.endpointWriteRepository.AddAsync(endpoint);
                await this.endpointWriteRepository.SaveAsync();
            }
            foreach (var role in endpoint.AppRoles)
                endpoint.AppRoles.Remove(role);

            var appRoles = await this.roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();
            foreach (var role in appRoles)
                endpoint.AppRoles.Add(role);     

            await this.endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code,string menu)
        {
            Endpoint? endpoint = await this.endpointReadRepository.Table
                .Include(e => e.AppRoles)
                .Include(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if(endpoint != null)
                return endpoint.AppRoles.Select(r => r.Name).ToList();
            return null;
        }
    }
}
