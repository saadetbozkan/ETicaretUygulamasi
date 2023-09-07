using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.RemoveOrder
{
    public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommandRequest, RemoveOrderCommandResponse>
    {
        readonly IOrderService orderService;

        public RemoveOrderCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<RemoveOrderCommandResponse> Handle(RemoveOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await this.orderService.RemoveOrderAsync(request.Id);
            return new();
           
        }
    }
}
