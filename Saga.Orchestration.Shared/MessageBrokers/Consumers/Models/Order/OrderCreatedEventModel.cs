using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order
{
    public class OrderCreatedEventModel : IOrderCreatedEventModel
    {
        private readonly OrderSagaModel _orderSagaModel;

        public OrderCreatedEventModel(OrderSagaModel orderSagaModel)
        {
            _orderSagaModel = orderSagaModel;
            CreatedDate = DateTime.Now;
        }

        public Guid CorrelationId => _orderSagaModel.CorrelationId;

        public int OrderId => _orderSagaModel.OrderId;

        public int ProductId => _orderSagaModel.ProductId;

        public int Quantity => _orderSagaModel.Quantity;

        public DateTime CreatedDate { get; set; }
    }
}
