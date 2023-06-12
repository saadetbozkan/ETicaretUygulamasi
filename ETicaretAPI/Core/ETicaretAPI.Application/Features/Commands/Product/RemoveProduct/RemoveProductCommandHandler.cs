using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly ILogger<RemoveProductCommandHandler> logger;

        public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<RemoveProductCommandHandler> logger)
        {
            this.productWriteRepository = productWriteRepository;
            this.logger = logger;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await this.productWriteRepository.RemoveAsync(request.Id);
            await this.productWriteRepository.SaveAsync();
            this.logger.LogInformation("Ürün silindi.");
            return new();
        }
    }
}
