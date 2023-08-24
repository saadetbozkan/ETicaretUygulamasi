using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model );
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUserAysnc(int page, int size);
        Task<ListUser> GetUserByNameAsync(string name);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        int TotalUsersCount { get; }
        Task<bool> HasRolePermissionToEndPointAsync(string name, string code);
        Task<List<OrderListWithBasketItem>> GetOrdersToUserAsync(string username);
        Task<List<OrderListWithBasketItem>> GetOrdersToCurrentUserAsync();

    }
}
