using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var totalCount = this.productReadRepository.GetAll(false).Count();
            var products = this.productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate
            }).Skip(request.Page * request.Size).Take(request.Size);
            this.logger.LogInformation("Tüm ürünler listelendi.");
            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
