using ETicaretAPI.Application.DTOs.Order;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetOrdersToCurrentUser
{
    public class GetOrdersToCurrentUserQueryResponse
    {
        
        public List<OrderListWithBasketItem> OrderListWithBasketItemList { get; set; }
      
    }
}