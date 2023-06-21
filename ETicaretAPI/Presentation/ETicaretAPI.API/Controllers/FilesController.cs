using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration configuration;

        public FilesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("[action]")]
        public IActionResult GetBaseStorageUrl()
        {
            return Ok(new
            {
                Url = this.configuration["BaseStorageUrl"]
            });
        }
    }
}
