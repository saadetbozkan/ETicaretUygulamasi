using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLoginUser
{
    public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
    {
        readonly IAuthService authService;

        public GoogleLoginUserCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await authService.GoogleLoginAsync(request.IdToken, 60);

            return new()
            {
                Token = token
            };
        }
    }
}
