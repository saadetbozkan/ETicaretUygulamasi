using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        readonly IUserService userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await this.userService.DeleteUserAsync(request.Id);

            return new();
            
        }
    }
}
