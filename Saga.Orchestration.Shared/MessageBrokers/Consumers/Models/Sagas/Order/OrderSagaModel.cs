using Automatonymous;
using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order
{
    public class OrderSagaModel : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public State CurrentState { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
