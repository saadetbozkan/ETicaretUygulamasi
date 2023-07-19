using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        readonly IRoleService roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await this.roleService.UpdateRoleAsync(request.Id, request.Name);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
