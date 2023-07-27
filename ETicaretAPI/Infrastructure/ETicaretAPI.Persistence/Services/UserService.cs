﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> userManager;
        readonly IEndpointReadRepository endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
        {
            this.userManager = userManager;
            this.endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await this.userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";
            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await this.userManager.UpdateAsync(user);
            }
           // else
                //throw new NotFoundUserException();

        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await this.userManager.FindByIdAsync(userId);
            if(user != null)
            {
                resetToken = resetToken.UrlDecode();
                var result = await this.userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();

            }
        }

        public async Task<List<ListUser>> GetAllUserAysnc(int page, int size)
        {
            var users = await this.userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                IsAdmin = user.IsAdmin 
            }).ToList();
        }

        public int TotalUsersCount => this.userManager.Users.Count();

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await this.userManager.FindByIdAsync(userId);
            if(user is not null)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                await this.userManager.RemoveFromRolesAsync(user, userRoles);

                await this.userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await this.userManager.FindByIdAsync(userIdOrName);
            
            if (user is null)
                user = await this.userManager.FindByNameAsync(userIdOrName);

            if(user is not null)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            throw new NotFoundUserException("Kullanıcı bulunmamaktadır.");
        }

        public async Task<bool> HasRolePermissionToEndPointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            
            if(!userRoles.Any())
                return false;

           Endpoint? endpoint = await this.endpointReadRepository.Table
                .Include(e => e.AppRoles)
                .FirstOrDefaultAsync(e => e.Code == code);
            
            if (endpoint is null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.AppRoles.Select(r => r.Name);

            foreach (var userRole in userRoles)
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;

            return hasRole;
        }

        public async Task<ListUser> GetUserByNameAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            if (user is not null)

                return new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    NameSurname = user.NameSurname,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserName = user.UserName,
                    IsAdmin = user.IsAdmin,
                };
            return new();
            
        }
    }
}
