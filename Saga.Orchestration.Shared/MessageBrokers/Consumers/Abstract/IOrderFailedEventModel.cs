using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IOrderFailedEventModel
    {
        public Guid CorrelationId { get; set; }

        public int OrderId { get; set; }
    }
}
