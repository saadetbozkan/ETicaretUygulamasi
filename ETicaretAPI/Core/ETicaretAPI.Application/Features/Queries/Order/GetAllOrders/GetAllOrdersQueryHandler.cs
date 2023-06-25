using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var (orderCount, data) = await this.orderService.GetAllOrdersAsync(request.Page,request.Size);
   
            return new() {
                TotalOrderCount = orderCount,
                Orders = data,
            };
        }
    }
}
