using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IUpdateStockEventModel
    {
        public Guid CorrelationId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }
    }
}
