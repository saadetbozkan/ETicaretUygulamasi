using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly ILogger<CreateProductCommandHandler> logger;
        readonly IProductHubService productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<CreateProductCommandHandler> logger, IProductHubService productHubService)
        {
            this.productWriteRepository = productWriteRepository;
            this.logger = logger;
            this.productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await this.productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await this.productWriteRepository.SaveAsync();
            this.logger.LogInformation("Ürün eklendi.");
            await this.productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new CreateProductCommandResponse();
        }
    }
}
