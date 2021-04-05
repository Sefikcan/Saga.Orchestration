using Saga.Orchestration.Core.MessageBrokers.Models;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order
{
    public class OrderCompletedEventModel : EventModel, IOrderCompletedEventModel
    {
        public int OrderId { get; set; }
    }
}
