using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServiceController : ControllerBase
    {
        readonly IApplicationService applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstansts.AplicationService, ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints" )]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas = this.applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
