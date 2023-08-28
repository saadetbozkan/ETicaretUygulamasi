using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository productReadRepository;
        readonly ILogger<GetByIdProductQueryHandler> logger;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetByIdProductQueryHandler> logger)
        {
            this.productReadRepository = productReadRepository;
            this.logger = logger;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await this.productReadRepository.GetByIdAsync(request.Id, false);
            this.logger.LogInformation(product.Id + " ürünü listelendi.");
            return new()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CreatedDate = product.CreateDate,
                UpdatedDate = product.UpdateDate,
            };
        }
    }
}
