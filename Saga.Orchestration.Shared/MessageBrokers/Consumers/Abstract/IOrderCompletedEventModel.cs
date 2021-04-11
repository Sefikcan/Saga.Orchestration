using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IOrderCompletedEventModel
    {
        public Guid CorrelationId { get; }
        public int OrderId { get; }
    }
}
