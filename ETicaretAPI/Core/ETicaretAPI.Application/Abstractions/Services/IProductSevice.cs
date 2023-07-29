namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductSevice
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
    }
}
