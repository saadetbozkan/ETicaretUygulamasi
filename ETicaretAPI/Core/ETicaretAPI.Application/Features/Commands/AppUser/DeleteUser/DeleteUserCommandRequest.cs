using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.DeleteUser
{
    public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
    {
        public string Id { get; set; }
    }
}