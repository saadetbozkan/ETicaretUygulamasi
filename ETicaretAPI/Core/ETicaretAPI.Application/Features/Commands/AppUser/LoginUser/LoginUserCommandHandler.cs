using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
             Token token = await this.authService.LoginAsync(request.UserNameOrEmail, request.Password,60);
            return new()
            {
                Token = token
            };
        }
    }
}
