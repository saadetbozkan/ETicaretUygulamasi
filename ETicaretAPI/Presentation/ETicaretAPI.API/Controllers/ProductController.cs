using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository productWriteRepository;
        readonly private IProductReadRepository productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await this.productWriteRepository.AddAsync(new(){ Name = "Product 99", Price = 5f, Stock = 10 , CreateDate = DateTime.UtcNow});
             await this.productWriteRepository.SaveAsync();
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await this.productReadRepository.GetByIdAsync(id, false);
            product.Name = "p77";
            await this.productWriteRepository.SaveAsync();

            return Ok(product);
        }
    }
}
