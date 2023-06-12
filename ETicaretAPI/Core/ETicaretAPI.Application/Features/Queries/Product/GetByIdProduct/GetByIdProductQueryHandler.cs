using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.logger.LogInformation("Ürün listelendi.");
            return new()
            {
                Product = product
            };
        }
    }
}
