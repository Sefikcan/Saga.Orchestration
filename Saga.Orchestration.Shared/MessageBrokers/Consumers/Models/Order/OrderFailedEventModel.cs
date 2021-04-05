using Saga.Orchestration.Core.MessageBrokers.Models;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order
{
    public class OrderFailedEventModel : EventModel, IOrderFailedEventModel
    {
        public int OrderId { get; set; }
    }
}
