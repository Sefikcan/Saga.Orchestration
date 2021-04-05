using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipment.Services.Commands;
using Shipment.Services.DTO.Request;
using System.Threading.Tasks;

namespace Shipment.Api.Controllers
{
    [Route("api/shipments")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Shipment
        /// </summary>
        /// <param name="increaseStockRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentRequestModel createShipmentRequest)
        {
            var response = await _mediator.Send(new CreateShipmentCommand(createShipmentRequest));
            return Ok(response);
        }
    }
}
