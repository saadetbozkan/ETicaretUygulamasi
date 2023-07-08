using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.ComplatedOrder
{
    public class ComplatedOrderCommandRequest : IRequest<ComplatedOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}