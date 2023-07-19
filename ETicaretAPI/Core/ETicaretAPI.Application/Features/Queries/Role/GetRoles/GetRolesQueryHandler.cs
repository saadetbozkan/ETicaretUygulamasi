using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetRoles
{

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        readonly IRoleService roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var (totalRolesCount, datas) = this.roleService.GetAllRole(request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalRolesCount = totalRolesCount
            };
        }
    }
}
