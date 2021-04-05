namespace Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract
{
    public interface IOrderSagaEventModel
    {
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
