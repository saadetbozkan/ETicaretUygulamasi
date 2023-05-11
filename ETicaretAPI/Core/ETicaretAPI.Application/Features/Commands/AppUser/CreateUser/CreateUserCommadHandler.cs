using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommadHandler : IRequestHandler<CreateUserCommadRequest, CreateUserCommadResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> userManager;

        public CreateUserCommadHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<CreateUserCommadResponse> Handle(CreateUserCommadRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = request.NameSurname,
                UserName = request.UserName,
                Email = request.Email                
            },request.Password);

            CreateUserCommadResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach(var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";
            return response;
        }
    }
}
