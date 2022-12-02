using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository productWriteRepository;
        readonly private IProductReadRepository productReadRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileService fileService;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]Pagination pagination)
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
            }).Skip(pagination.Page*pagination.Size).Take(pagination.Size);

            return Ok(new
            {
                totalCount,
                products
            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await this.productReadRepository.GetByIdAsync(id, false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await this.productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await this.productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await this.productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await this.productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            
            await this.productWriteRepository.RemoveAsync(id);
            await this.productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
           await this.fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
        }

    }
}
