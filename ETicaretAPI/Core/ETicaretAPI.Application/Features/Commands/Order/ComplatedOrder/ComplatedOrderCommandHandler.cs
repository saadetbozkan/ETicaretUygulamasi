using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.ComplatedOrder
{
    public class ComplatedOrderCommandHandler : IRequestHandler<ComplatedOrderCommandRequest, ComplatedOrderCommandResponse>
    {
        readonly IOrderService orderService;

        public ComplatedOrderCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<ComplatedOrderCommandResponse> Handle(ComplatedOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await orderService.ComplatedOrderAsync(request.Id);
            return new();
        }
    }
}