using ETicaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebookLoginUser
{
    public class FacebookLoginUserCommandRequest: IRequest<FacebookLoginUserCommandResponse>
    {
        public string AuthToken { get; set; }
    }
}
