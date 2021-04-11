using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment
{
    public class CreateShipmentEventModel : ICreateShipmentEventModel
    {
        private readonly OrderSagaModel _orderSagaModel;

        public CreateShipmentEventModel(OrderSagaModel orderSagaModel)
        {
            _orderSagaModel = orderSagaModel;
            ShipmentType = (int)Core.Enums.ShipmentType.MNG;
            CreatedDate = DateTime.Now;
        }

        public Guid CorrelationId => _orderSagaModel.CorrelationId;

        public int OrderId => _orderSagaModel.OrderId;

        public int ShipmentType { get; }

        public DateTime CreatedDate { get; }
    }
}
