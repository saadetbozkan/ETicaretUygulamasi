using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService authService;
        readonly ILogger<LoginUserCommandHandler> logger;

        public LoginUserCommandHandler(IAuthService authService, ILogger<LoginUserCommandHandler> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
             Token token = await this.authService.LoginAsync(request.UserNameOrEmail, request.Password, 60*30);
            this.logger.LogInformation("Kullanıcı giriş yaptı.");
            return new()
            {
                Token = token
            };
        }
    }
}
