using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface ICreateShipmentEventModel
    {
        public Guid CorrelationId { get; }

        public int OrderId { get; }

        public int ShipmentType { get; }
    }
}
