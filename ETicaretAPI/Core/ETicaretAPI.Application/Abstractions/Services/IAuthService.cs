﻿using ETicaretAPI.Application.Abstractions.Services.Authentications;
using System.Globalization;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IAuthService: IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId); 
    }
}
