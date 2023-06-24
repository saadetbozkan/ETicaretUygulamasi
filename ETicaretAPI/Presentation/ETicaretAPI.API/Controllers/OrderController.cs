using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrderController : ControllerBase
    {
        readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response =  await this.mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
       
    }
}
