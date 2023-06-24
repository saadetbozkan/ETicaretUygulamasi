using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService userService;
        readonly ILogger<CreateUserCommandHandler> logger;

        public CreateUserCommandHandler(IUserService userService, ILogger<CreateUserCommandHandler> logger)
        {
            this.userService = userService;
            this.logger = logger;
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
            this.logger.LogInformation("Kullanıcı oluşturuldu.");
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };;
            
        }
    }
}
