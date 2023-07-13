using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.ComplatedOrder
{
    public class ComplatedOrderCommandHandler : IRequestHandler<ComplatedOrderCommandRequest, ComplatedOrderCommandResponse>
    {
        readonly IOrderService orderService;
        readonly IMailService mailService;

        public ComplatedOrderCommandHandler(IOrderService orderService, IMailService mailService = null)
        {
            this.orderService = orderService;
            this.mailService = mailService;
        }

        public async Task<ComplatedOrderCommandResponse> Handle(ComplatedOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded, CompletedOrderDTO dto) = await orderService.ComplatedOrderAsync(request.Id);
            if (succeeded) 
               await this.mailService.SendComplatedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.UserNameSurname);

            return new();
        }
    }
}