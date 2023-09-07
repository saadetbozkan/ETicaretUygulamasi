using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.RemoveOrder
{
    public class RemoveOrderCommandRequest: IRequest<RemoveOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}