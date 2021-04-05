using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Stock.Services.Extensions
{
    public static class RegisterStockService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
