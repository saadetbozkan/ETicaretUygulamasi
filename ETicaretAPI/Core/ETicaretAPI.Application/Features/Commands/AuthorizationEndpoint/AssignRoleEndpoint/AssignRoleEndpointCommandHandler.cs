using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
    {
        readonly IAuthorizationEndpointService authorizationEndpointService;

        public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            this.authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
          await this.authorizationEndpointService.AuthorizationEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);

            return new();
        }
    }
}
