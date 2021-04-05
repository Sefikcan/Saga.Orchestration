using Saga.Orchestration.Core.MessageBrokers.Models;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;

namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Stock
{
    public class UpdateStockEventModel : EventModel, IUpdateStockEventModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }
    }
}
