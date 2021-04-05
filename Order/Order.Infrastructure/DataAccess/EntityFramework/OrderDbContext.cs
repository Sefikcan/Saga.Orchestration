using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.DataAccess.EntityFramework
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Order> Orders { get; set; }
    }
}
