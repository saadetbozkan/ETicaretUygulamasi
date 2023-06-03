using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await this.userService.CreateAsync(new()
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
                NameSurname = request.NameSurname,
                PasswordConfirm = request.PasswordConfirm
            });
            
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };;
            
        }
    }
}
