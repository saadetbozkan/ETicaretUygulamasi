using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
    {
        readonly IBasketService basketService;

        public UpdateQuantityCommandHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await this.basketService.UpdateQentityAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });
            return new();
        }
    }
}
