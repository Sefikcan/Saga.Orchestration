using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shipment.Services.Extensions
{
    public static class RegisterShipmentService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
