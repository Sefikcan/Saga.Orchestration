using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.DataAccess.EntityFramework
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Stock> Stocks { get; set; }
    }
}
