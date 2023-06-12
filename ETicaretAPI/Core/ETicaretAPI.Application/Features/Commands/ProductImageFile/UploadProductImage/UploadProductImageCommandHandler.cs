using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService storageService;
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;
        readonly IProductReadRepository productReadRepository;
        readonly ILogger<UploadProductImageCommandHandler> logger;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, ILogger<UploadProductImageCommandHandler> logger)
        {
            this.storageService = storageService;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.productReadRepository = productReadRepository;
            this.logger = logger;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await this.storageService.UploadAsync("photo-images", request.Files);
            Domain.Entities.Product product = await this.productReadRepository.GetByIdAsync(request.Id);

            await this.productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = this.storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());
            await this.productImageFileWriteRepository.SaveAsync();
            this.logger.LogInformation("Ürünün resmi güncellendi.");
            return new();
        }
    }
}
