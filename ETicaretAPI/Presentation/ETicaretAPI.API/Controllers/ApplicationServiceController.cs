using ETicaretAPI.Application.Abstractions.Services.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ApplicationServiceController : ControllerBase
    {
        readonly IApplicationService applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas = this.applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
