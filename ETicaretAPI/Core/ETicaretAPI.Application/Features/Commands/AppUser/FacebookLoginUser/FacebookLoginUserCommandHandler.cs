﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
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
        readonly ILogger<FacebookLoginUserCommandHandler> logger;

        public FacebookLoginUserCommandHandler(IAuthService authService, ILogger<FacebookLoginUserCommandHandler> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            Token token = await authService.FacebookLoginAsync(request.AuthToken, 60);
            this.logger.LogInformation("Kullanıcı facebook ile giriş yaptı.");

            return new()
            {
                Token = token
            };
        }
    }
}