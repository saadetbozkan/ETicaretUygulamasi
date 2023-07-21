using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserService userService;

        public GetRolesToUserQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
            var datas= await this.userService.GetRolesToUserAsync(request.UserId);
            return new()
            {
                Roles = datas,
            };
        }
    }
}
