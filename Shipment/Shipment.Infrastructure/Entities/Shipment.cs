using Saga.Orchestration.Core.Entity;

namespace Shipment.Infrastructure.Entities
{
    public class Shipment : IEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ShipmentType { get; set; }
    }
}
