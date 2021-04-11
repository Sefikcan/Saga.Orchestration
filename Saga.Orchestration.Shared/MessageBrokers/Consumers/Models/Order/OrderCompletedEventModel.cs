using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order
{
    public class OrderCompletedEventModel : IOrderCompletedEventModel
    {
        private readonly OrderSagaModel _orderSagaModel;

        public OrderCompletedEventModel(OrderSagaModel orderSagaModel)
        {
            _orderSagaModel = orderSagaModel;
            CreatedDate = DateTime.Now;
        }

        public Guid CorrelationId => _orderSagaModel.CorrelationId;

        public int OrderId => _orderSagaModel.OrderId;

        public DateTime CreatedDate { get; }
    }
}
