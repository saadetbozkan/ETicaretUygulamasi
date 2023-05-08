using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;
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

        public UploadProductImageCommandHandler(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository)
        {
            this.storageService = storageService;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.productReadRepository = productReadRepository;
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
            return new();
        }
    }
}
