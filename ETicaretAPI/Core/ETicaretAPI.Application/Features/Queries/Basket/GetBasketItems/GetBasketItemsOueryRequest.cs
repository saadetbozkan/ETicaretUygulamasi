using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsOueryRequest : IRequest<List<GetBasketItemsOueryResponse>>
    {
    }
}
