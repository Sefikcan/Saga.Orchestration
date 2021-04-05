using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.DataAccess.EntityFramework;
using Saga.Orchestration.Core.Settings.Concrete.Databases;

namespace Order.Infrastructure.Extensions
{
    public static class RegisterOrderInfra
    {
        public static IServiceCollection AddOrderInfra(this IServiceCollection services, IConfiguration configuration)
        {
            OrderDbSettings orderDbSettings = new OrderDbSettings();
            configuration.GetSection(nameof(OrderDbSettings)).Bind(orderDbSettings);
            services.AddSingleton(orderDbSettings);

            services.AddDbContext<OrderDbContext>(c =>
                c.UseSqlServer(orderDbSettings.ConnectionStrings), ServiceLifetime.Transient);

            return services;
        }
    }
}
