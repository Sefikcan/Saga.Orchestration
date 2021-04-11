using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IUpdateStockEventModel
    {
        public Guid CorrelationId { get; }

        public int ProductId { get; }

        public int Quantity { get; }

        public int OrderId { get; }

        public DateTime CreatedDate { get; }
    }
}
