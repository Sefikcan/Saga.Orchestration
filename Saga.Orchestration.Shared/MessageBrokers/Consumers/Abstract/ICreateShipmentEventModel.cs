using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface ICreateShipmentEventModel
    {
        public Guid CorrelationId { get; set; }

        public int OrderId { get; set; }

        public int ShipmentType { get; set; }
    }
}
