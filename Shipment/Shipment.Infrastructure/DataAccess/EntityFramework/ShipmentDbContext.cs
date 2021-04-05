using Microsoft.EntityFrameworkCore;

namespace Shipment.Infrastructure.DataAccess.EntityFramework
{
    public class ShipmentDbContext : DbContext
    {
        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Shipment> Shipments { get; set; }
    }
}
