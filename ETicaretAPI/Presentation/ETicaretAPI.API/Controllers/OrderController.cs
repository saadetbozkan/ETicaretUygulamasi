
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Order.ComplatedOrder;
using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Features.Commands.Order.RemoveOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetAllOrders;
using ETicaretAPI.Application.Features.Queries.Order.GetOrderById;
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

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Order, ActionType = ActionType.Reading
            , Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await this.mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Order, ActionType = ActionType.Reading
            , Definition = "Get Order By Id ")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await this.mediator.Send(getOrderByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Order, ActionType = ActionType.Writing
            , Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response =  await this.mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        [HttpGet("complated-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Order, ActionType = ActionType.Updating
            , Definition = "Complate Order")]
        public async Task<IActionResult> ComplatedOrder([FromRoute] ComplatedOrderCommandRequest complatedOrderCommandRequest)
        {
            ComplatedOrderCommandResponse response = await this.mediator.Send(complatedOrderCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstansts.Order, ActionType = ActionType.Deleting, Definition = "Delete Order")]
        public async Task<IActionResult> Delete([FromRoute] RemoveOrderCommandRequest removeOrderCommandRequest)
        {

            RemoveOrderCommandResponse response = await this.mediator.Send(removeOrderCommandRequest);
            return Ok();
        }
    }
}
