using ETicaretAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ETicaretAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizationEndpointController : ControllerBase
    {
        readonly IMediator mediator;

        public AuthorizationEndpointController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("get-roles-to-endpoint")]
        public async Task<IActionResult> GetRolesToEndpoint([FromBody]GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            GetRolesToEndpointQueryResponse response = await this.mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint([FromBody]AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response = await this.mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }
    }
}
