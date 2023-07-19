using Azure;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.DTOs.Configuration;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Role.CreateRole;
using ETicaretAPI.Application.Features.Commands.Role.DeleteRole;
using ETicaretAPI.Application.Features.Commands.Role.UpdateRole;
using ETicaretAPI.Application.Features.Queries.Role.GetRoleById;
using ETicaretAPI.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin") ]
    public class RoleController : ControllerBase
    {
        readonly IMediator mediator;

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Role, ActionType = ActionType.Reading, Definition = "Get Roles")]
        public async Task<IActionResult> GetRolesAsync([FromQuery]GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse response = await this.mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Role, ActionType = ActionType.Reading, Definition = "Get Role By Id")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await this.mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Role, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await this.mediator.Send(createRoleCommandRequest);
            return Ok(response);

        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Role, ActionType = ActionType.Updating, Definition = "Update Role")]
        public async Task<IActionResult> UpdateRole([FromBody,FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest )
        {
            UpdateRoleCommandResponse response = await this.mediator.Send(updateRoleCommandRequest);
            return Ok(response);

        }

        [HttpDelete("{Name}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Role, ActionType = ActionType.Deleting, Definition = "Delete Role")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await this.mediator.Send(deleteRoleCommandRequest);
            return Ok(response);

        }
    }
}
