using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        readonly IAuthorizationEndpointService authorizationEndpointService;

        public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            this.authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await this.authorizationEndpointService.GetRolesToEndpointAsync(request.Code, request.Menu);
            return new()
            {
                Roles = datas
            };
        }
    }
}
