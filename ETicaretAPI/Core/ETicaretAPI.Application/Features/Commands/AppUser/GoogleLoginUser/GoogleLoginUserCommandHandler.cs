using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLoginUser
{
    public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
    {
        readonly IAuthService authService;
        readonly ILogger<GoogleLoginUserCommandHandler> logger;

        public GoogleLoginUserCommandHandler(IAuthService authService, ILogger<GoogleLoginUserCommandHandler> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await authService.GoogleLoginAsync(request.IdToken, 60*15);
            this.logger.LogInformation("Kullanıcı google ile giriş yaptı.");

            return new()
            {
                Token = token
            };
        }
    }
}
