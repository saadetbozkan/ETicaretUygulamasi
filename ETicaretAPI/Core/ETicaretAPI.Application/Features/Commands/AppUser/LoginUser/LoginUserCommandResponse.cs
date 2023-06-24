using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
    }

}
