﻿using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Features.Commands.AppUser.AssignRoleToUser;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Application.Features.Commands.AppUser.DeleteUser;
using ETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword;
using ETicaretAPI.Application.Features.Queries.AppUser.GetAllUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GetOrdersToCurrentUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await this.mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await this.mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get All User", Menu = AuthorizeDefinitionConstansts.User)]
        public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await this.mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Updating, Definition = "Assign Role To User", Menu = AuthorizeDefinitionConstansts.User)]
        public async Task<IActionResult> AssignRoleToUser([FromBody]AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await this.mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Roles To User", Menu = AuthorizeDefinitionConstansts.User)]
        public async Task<IActionResult> GetRolesToUser([FromRoute]GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await this.mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);

        }

        [HttpGet("get-orders-to-current-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Orders To Current User", Menu = AuthorizeDefinitionConstansts.User)]
        public async Task<IActionResult> GetOrdersToCurrentUser([FromQuery]GetOrdersToCurrentUserQueryRequest getOrdersToCurrentUserQueryRequest)
        {
            GetOrdersToCurrentUserQueryResponse response = await this.mediator.Send(getOrdersToCurrentUserQueryRequest);
            return Ok(response);

        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.User, ActionType = Application.Enums.ActionType.Deleting, Definition = "Delete User")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteUserCommandRequest deleteUserCommandRequest)
        {
            DeleteUserCommandResponse response = await this.mediator.Send(deleteUserCommandRequest);
            return Ok(response);

        }

    }
}

