using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebookLoginUser
{
    public class FacebookLoginUserCommandRequest: IRequest<FacebookLoginUserCommandResponse>
    {
        public string AuthToken { get; set; }
    }
}
