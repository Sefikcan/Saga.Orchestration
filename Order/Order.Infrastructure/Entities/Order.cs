using Saga.Orchestration.Core.Entity;

namespace Order.Infrastructure.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
