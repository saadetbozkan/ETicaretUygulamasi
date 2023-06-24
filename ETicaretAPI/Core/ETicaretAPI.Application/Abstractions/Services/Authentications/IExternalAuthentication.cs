﻿namespace ETicaretAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime);
        Task<DTOs.Token> FacebookLoginAsync(string AuthToken, int accessTokenLifeTime);
    }
}
