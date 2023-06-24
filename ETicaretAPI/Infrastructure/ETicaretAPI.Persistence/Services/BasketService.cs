using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Basket;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly UserManager<AppUser> userManager;
        readonly IOrderReadRepository orderReadRepository;
        readonly IBasketWriteRepository basketWriteRepository;
        readonly IBasketItemWriteRepository basketItemWriteRepository;
        readonly IBasketItemReadRepository basketItemReadRepository;
        readonly IBasketReadRepository basketReadRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.orderReadRepository = orderReadRepository;
            this.basketWriteRepository = basketWriteRepository;
            this.basketItemWriteRepository = basketItemWriteRepository;
            this.basketItemReadRepository = basketItemReadRepository;
            this.basketReadRepository = basketReadRepository;
        }

        private async Task<Basket?> ContextUser()
        {
            var username = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var _basket = from basket in user?.Baskets
                              join order in this.orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }
                await this.basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hata ile karşılaşıldı.");
        }
        public async Task AddItemToBasketAsync(CreateBasketItem basketItem)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem _basketItem = await this.basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem != null)
                    _basketItem.Quantity++;
                else
                    await this.basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,

                    });
                await this.basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();
            Basket? result = await this.basketReadRepository.Table.Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await this.basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                this.basketItemWriteRepository.Remove(basketItem);
                await this.basketItemWriteRepository.SaveAsync();
            }

        }

        public async Task UpdateQentityAsync(UpdateBasketItem basketItem)
        {
            BasketItem? _basketItem = await basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await this.basketItemWriteRepository.SaveAsync();
            }
        }
        public Basket? GetUserActiveBasket
            {
                get
                {
                    Basket? basket = ContextUser().Result; 
                    return basket;
                }
              
            }

    }
}
