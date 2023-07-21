using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetAllUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await this.userService.GetAllUserAysnc(request.Page, request.Size);
            return new()
            {
                Users = datas,
                TotalUsersCount = this.userService.TotalUsersCount
            };

        }
    }
}
