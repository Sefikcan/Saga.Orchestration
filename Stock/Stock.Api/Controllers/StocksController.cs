using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.Commands;
using Stock.Services.DTO.Request;
using System.Threading.Tasks;

namespace Stock.Api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Increase Stock Operation
        /// </summary>
        /// <param name="increaseStockRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("increase-stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockRequestModel increaseStockRequest)
        {
            var response = await _mediator.Send(new IncreaseStockCommand(increaseStockRequest));
            return Ok(response);
        }

        /// <summary>
        /// Decrease Stock Operation
        /// </summary>
        /// <param name="decreaseStockRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("decrease-stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockRequestModel decreaseStockRequest)
        {
            var response = await _mediator.Send(new DecreaseStockCommand(decreaseStockRequest));
            return Ok(response);
        }
    }
}
