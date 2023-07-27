using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService userService;

        public RolePermissionFilter(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;
            if (!string.IsNullOrEmpty(name) && !(await this.userService.GetUserByNameAsync(name)).IsAdmin)
            {
                var descirptor =  context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descirptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute))  as AuthorizeDefinitionAttribute;

                var htttpAttribute = descirptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                var code = $"{(htttpAttribute != null ? htttpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

                var hasRole = await this.userService.HasRolePermissionToEndPointAsync(name, code);

                if (!hasRole)
                    context.Result = new UnauthorizedResult();
                else
                    await next();                
            }
            else
                await next();
        }
    }
}
