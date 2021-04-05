using Saga.Orchestration.Core.MessageBrokers.Models;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order
{
    public class OrderSagaEventModel : EventModel, IOrderSagaEventModel
    {
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
