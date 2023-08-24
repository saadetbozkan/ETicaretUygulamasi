using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetOrdersToCurrentUser
{
    public class GetOrdersToCurrentUserQueryHandler : IRequestHandler<GetOrdersToCurrentUserQueryRequest, GetOrdersToCurrentUserQueryResponse>
    {
        readonly IUserService userService;

        public GetOrdersToCurrentUserQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetOrdersToCurrentUserQueryResponse> Handle(GetOrdersToCurrentUserQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await userService.GetOrdersToCurrentUserAsync();

            return new()
            {
                OrderListWithBasketItemList = data
            };

        
        }
    }
}
