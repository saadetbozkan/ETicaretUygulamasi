using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse> 
    {
        readonly IProductReadRepository productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            this.productReadRepository = productReadRepository;
            this.logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var totalProductCount = this.productReadRepository.GetAll(false).Count();
            var products = this.productReadRepository.GetAll(false)
                .Include(p => p.ProductImageFiles)
                .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate,
                p.ProductImageFiles
            }).Skip(request.Page * request.Size).Take(request.Size);
            this.logger.LogInformation("Tüm ürünler listelendi.");
            return new()
            {
                Products = products,
                TotalProductCount = totalProductCount
            };
        }
    }
}
