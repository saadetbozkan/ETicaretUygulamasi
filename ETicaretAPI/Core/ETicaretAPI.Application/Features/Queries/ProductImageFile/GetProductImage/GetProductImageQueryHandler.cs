using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository productReadRepository;
        readonly ILogger<GetProductImageQueryHandler> logger;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, ILogger<GetProductImageQueryHandler> logger)
        {
            this.productReadRepository = productReadRepository;
            this.logger = logger;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await this.productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            this.logger.LogInformation("Ürünün resimleri listelendi.");

            return product?.ProductImageFiles.Select(p => new GetProductImageQueryResponse
                {

                    Id = p.Id,
                    Path = Path.Combine("https://localhost:7125", p.Path),
                    FileName= p.FileName
                }).ToList();
        }
    }
}
