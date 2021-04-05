using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Services.Extensions
{
    public static class RegisterOrderService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //EasyNetQSettings easyNetQSettings = new EasyNetQSettings();
            //configuration.GetSection(nameof(EasyNetQSettings)).Bind(easyNetQSettings);
            //services.AddSingleton(easyNetQSettings);

            //var bus = RabbitHutch.CreateBus(easyNetQSettings.Uri);
            //services.AddSingleton(bus);

            return services;
        }
    }
}
