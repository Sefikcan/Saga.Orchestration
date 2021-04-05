using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Orchestration.Core.Settings.Concrete.Databases;
using Shipment.Infrastructure.DataAccess.EntityFramework;

namespace Shipment.Infrastructure.Extensions
{
    public static class RegisterShipmentInfra
    {
        public static IServiceCollection AddShipmentInfra(this IServiceCollection services, IConfiguration configuration)
        {
            ShipmentDbSettings shipmentDbSettings = new ShipmentDbSettings();
            configuration.GetSection(nameof(ShipmentDbSettings)).Bind(shipmentDbSettings);
            services.AddSingleton(shipmentDbSettings);

            services.AddDbContext<ShipmentDbContext>(c =>
                c.UseSqlServer(shipmentDbSettings.ConnectionStrings), ServiceLifetime.Transient);

            return services;
        }
    }
}
