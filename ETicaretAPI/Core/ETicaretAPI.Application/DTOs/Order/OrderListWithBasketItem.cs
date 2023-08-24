using System.Xml.Linq;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class OrderListWithBasketItem
    {
        public string OrderCode { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<Object> BasketItems { get; set; }
        public bool IsComplated { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
