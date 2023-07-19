﻿using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        readonly IRoleService roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await this.roleService.DeleteRoleAsync(request.Name);
            return new()
            {
                Succeeded = result
            };           
        }
    }
}
