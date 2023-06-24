using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        readonly IBasketService basketService;

        public AddItemToBasketCommandHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await this.basketService.AddItemToBasketAsync(new()
            {
                ProductId = request.ProductId,
                Quantity =  request.Quantity
            });
            return new();
        }
    }
}
