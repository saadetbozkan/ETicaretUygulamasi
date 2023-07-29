using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Exceptions.ProductException;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using System.Text.Json;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductService : IProductSevice
    {
        readonly IProductReadRepository productReadRepository;
        readonly IQRCodeService qrCodeService;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService)
        {
            this.productReadRepository = productReadRepository;
            this.qrCodeService = qrCodeService;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            Product product = await this.productReadRepository.GetByIdAsync(productId);
            if (product is null)
                throw new NotFoundProductExpection("Product not found.");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreateDate
            };
            string plainText = JsonSerializer.Serialize(plainObject);
             return  this.qrCodeService.GenerateQRCode(plainText);
        }
    }
}
