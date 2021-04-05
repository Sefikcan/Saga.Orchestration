using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IOrderCreatedEventModel
    {
        int OrderId { get; }

        int ProductId { get; }

        int Quantity { get; }

        Guid CorrelationId { get; }
    }
}
