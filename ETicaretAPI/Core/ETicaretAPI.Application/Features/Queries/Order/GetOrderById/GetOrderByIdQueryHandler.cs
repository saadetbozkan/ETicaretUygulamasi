using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        readonly IOrderService orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await this.orderService.GetOrderByIdAsync(request.Id);

            return new()
            {
                Id = data.Id,
                BasketItem =data.BasketItem,
                CreatedDate = data.CreatedDate,
                OrderCode = data.OrderCode,
                Address = data.Address,
                Description = data.Description,
                Complated = data.Complated
            };
        }
    }
}
