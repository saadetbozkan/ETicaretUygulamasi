using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Facebook;
using ETicaretAPI.Application.DTOs.NewFolder;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration configuration;
        readonly UserManager<AppUser> userManager;
        readonly ITokenHandler tokenHandler;
        readonly SignInManager<AppUser> signInManager;
        readonly HttpClient httpClient;
        readonly IUserService userService;
        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IHttpClientFactory httpClientFactory, IUserService userService)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.tokenHandler = tokenHandler;
            this.signInManager = signInManager;
            this.httpClient = httpClientFactory.CreateClient();
            this.userService = userService;
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await this.userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var identityResult = await this.userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await this.userManager.AddLoginAsync(user, info);
                Token token = tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await this.userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }

            throw new Exception("Invalid external Authentication.");       
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { configuration["ExternalLoginSettings:Google:Client_ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser user = await this.userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);           
        }

         async public Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await this.userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await this.userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
                throw new NotFoundUserException("Kullanıcı adı veya şifre hatalı...");

            SignInResult result = await this.signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = this.tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await this.userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }

            throw new AuthenticationErrorException();
        }

        public async Task<Token> FacebookLoginAsync(string AuthToken, int accessTokenLifeTime)
        {
            string accessTokenResponse = await this.httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={configuration["ExternalLoginSettings:Facebook:App-ID"]}&client_secret={configuration["ExternalLoginSettings:Facebook:App-Secret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

            string userAccessTokenValidation =await this.httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            FacebookUserAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfoResponse = await this.httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={AuthToken}");
                FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
           
                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                AppUser user = await this.userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime); 
            }
            
            throw new Exception("Invalid external Authentication.");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
           AppUser? user = await  this.userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
               Token token  = this.tokenHandler.CreateAccessToken(60, user);
               await this.userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }
            throw new NotFoundUserException();
        }
    }
}
