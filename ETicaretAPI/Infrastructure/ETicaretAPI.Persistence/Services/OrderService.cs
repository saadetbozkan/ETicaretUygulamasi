using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository orderWriteRepository;
        readonly IOrderReadRepository orderReadRepository;
        readonly IComplatedOrderWriteRepository complatedOrderWriteRepository;
        readonly IComplatedOrderReadRepository complatedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, IComplatedOrderWriteRepository complatedOrderWriteRepository, IComplatedOrderReadRepository complatedOrderReadRepository)
        {
            this.orderWriteRepository = orderWriteRepository;
            this.orderReadRepository = orderReadRepository;
            this.complatedOrderWriteRepository = complatedOrderWriteRepository;
            this.complatedOrderReadRepository = complatedOrderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(",") + 1, orderCode.Length - orderCode.IndexOf(",") - 1);
            await this.orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await this.orderWriteRepository.SaveAsync();
        }

        public async Task<(int,List<ListOrder>)> GetAllOrdersAsync(int page, int size)
        {
            var query = this.orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.ComplatedOrder)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product);
               
            var data = query.Skip(page * size).Take(size);

            var listOrders = await data.Select(o => new ListOrder
            {
                Id = (o.Id).ToString(),
                CreatedDate = o.CreateDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName,
                Complated = o.ComplatedOrder != null ? true : false
            }).ToListAsync();


            var orderCount = await query.CountAsync();

            return (orderCount, listOrders);
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await this.orderReadRepository.Table
                .Include(o => o.ComplatedOrder)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new()
            {
                Id = data.Id.ToString(),
                BasketItem = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data.Address,
                OrderCode = data.OrderCode,
                CreatedDate = data.CreateDate,
                Description = data.Description,
                Complated = data.ComplatedOrder != null ? true : false
            };
        }

        public async Task ComplatedOrderAsync(string id)
        {
            Order order = await this.orderReadRepository.GetByIdAsync(id);
            if(order != null)
            {
                await this.complatedOrderWriteRepository.AddAsync(new()
                {
                    OrderId = Guid.Parse(id)
                });
                await this.complatedOrderWriteRepository.SaveAsync();
            }
        }
    }
}
