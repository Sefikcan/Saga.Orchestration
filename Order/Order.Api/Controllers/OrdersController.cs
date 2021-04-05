using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Services.Commands;
using Order.Services.DTO.Request;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="createOrderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel createOrderRequest)
        {
            var response = await _mediator.Send(new CreateOrderCommand(createOrderRequest));
            return Ok(response);
        }
    }
}
