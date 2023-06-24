using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketCommandRequest :IRequest<RemoveBasketCommandResponse>
    {
        public string BasketItemId { get; set; }
    }
}
