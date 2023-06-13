using Serilog.Context;
using System.Runtime.CompilerServices;

namespace ETicaretAPI.API.Middlewares
{
    static public class ConfigureAuthenticatedUserLogExtension
     {
        public static void ConfigureAuthenticatedUserLog(this WebApplication application)
        {
            application.Use(async (context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("user_name", username);
                await next();
            });
        }
    }
}
