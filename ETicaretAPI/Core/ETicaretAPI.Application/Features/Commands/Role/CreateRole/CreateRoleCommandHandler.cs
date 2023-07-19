using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
        readonly IRoleService roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await this.roleService.CreateRoleAsync(request.Name);   
            return new()
            {
                Succeeded = result
            };
        }
    }
}
