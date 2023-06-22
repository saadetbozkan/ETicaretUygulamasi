using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketCommandHandler : IRequestHandler<RemoveBasketCommandRequest, RemoveBasketCommandResponse>
    {
        readonly IBasketService basketService;

        public RemoveBasketCommandHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<RemoveBasketCommandResponse> Handle(RemoveBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await this.basketService.RemoveBasketItemAsync(request.BasketItemId);
            return new();

        }
    }
}
