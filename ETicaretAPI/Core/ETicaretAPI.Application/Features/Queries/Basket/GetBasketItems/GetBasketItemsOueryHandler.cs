using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsOueryHandler : IRequestHandler<GetBasketItemsOueryRequest, List<GetBasketItemsOueryResponse>>
    {
        readonly IBasketService basketService;

        public GetBasketItemsOueryHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<List<GetBasketItemsOueryResponse>> Handle(GetBasketItemsOueryRequest request, CancellationToken cancellationToken)
        {
            List<BasketItem> basketItems = await this.basketService.GetBasketItemsAsync();

            return basketItems.Select(ba => new GetBasketItemsOueryResponse
            {
                BasketItemId = ba.Id.ToString(),
                Name = ba.Product.Name,
                Price = ba.Product.Price,
                Quantity = ba.Quantity
            }).ToList();     

        }
    }
}
