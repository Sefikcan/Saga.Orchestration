using Saga.Orchestration.Core.MessageBrokers.Models;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment
{
    public class CreateShipmentEventModel : EventModel, ICreateShipmentEventModel
    {
        public int OrderId { get; set; }

        public int ShipmentType { get; set; }
    }
}
