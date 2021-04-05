using Automatonymous;
using System;

namespace Saga.Consumer.OrderSaga
{
    public class OrderSagaModel : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set; }

        public State CurrentState { get; set; }

        public int OrderId { get; set; }
    }
}
