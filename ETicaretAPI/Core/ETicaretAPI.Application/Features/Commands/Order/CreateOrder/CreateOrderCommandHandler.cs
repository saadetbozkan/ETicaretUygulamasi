using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService orderService;
        readonly IBasketService basketService;
        readonly IOrderHubService orderHubService;
        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            this.orderService = orderService;
            this.basketService = basketService;
            this.orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await this.orderService.CreateOrderAsync(new()
            {
                BasketId = this.basketService.GetUserActiveBasket?.Id.ToString(),
                Address = request.Address,
                Description = request.Description,
            });
            await this.orderHubService.OrderAddedMessageAsync("Heeyy!!! Yeni bir sipariş geldi! :)");
            return new();

        }
    }
}
