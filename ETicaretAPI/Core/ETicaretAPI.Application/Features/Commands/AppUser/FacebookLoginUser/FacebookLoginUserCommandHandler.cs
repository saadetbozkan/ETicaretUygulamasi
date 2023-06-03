using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebookLoginUser
{
    public class FacebookLoginUserCommandHandler : IRequestHandler<FacebookLoginUserCommandRequest, FacebookLoginUserCommandResponse>
    {
        readonly IAuthService authService;

        public FacebookLoginUserCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            Token token = await authService.FacebookLoginAsync(request.AuthToken, 60);

            return new()
            {
                Token = token
            };
        }
    }
}
