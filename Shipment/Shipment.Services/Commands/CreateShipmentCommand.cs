using MediatR;
using Saga.Orchestration.Core.Mappings.Abstract;
using Shipment.Infrastructure.DataAccess.EntityFramework;
using Shipment.Services.DTO.Request;
using Shipment.Services.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Shipment.Services.Commands
{
    public class CreateShipmentCommand : IRequest<CreateShipmentResponseModel>
    {
        public CreateShipmentRequestModel CreateShipmentRequest { get; set; }

        public CreateShipmentCommand(CreateShipmentRequestModel createShipmentRequest)
        {
            CreateShipmentRequest = createShipmentRequest;
        }
    }

    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, CreateShipmentResponseModel>
    {
        private readonly ShipmentDbContext _dbContext;
        private readonly IMapping _mapping;

        public CreateShipmentCommandHandler(ShipmentDbContext dbContext,
            IMapping mapping)
        {
            _dbContext = dbContext;
            _mapping = mapping;
        }

        public async Task<CreateShipmentResponseModel> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = _mapping.Map<CreateShipmentRequestModel, Infrastructure.Entities.Shipment>(request.CreateShipmentRequest);

            var response = await _dbContext.AddAsync(shipment);
            await _dbContext.SaveChangesAsync();

            return _mapping.Map<Infrastructure.Entities.Shipment, CreateShipmentResponseModel>(response.Entity);
        }
    }
}
