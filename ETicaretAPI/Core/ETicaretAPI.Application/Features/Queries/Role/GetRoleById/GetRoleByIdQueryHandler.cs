using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        readonly IRoleService roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await this.roleService.GetRoleByIdAsync(request.Id);
            return new()
            {
                Id = result.id,
                Name = result.name
            };
        }
    }
}
