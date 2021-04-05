using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Orchestration.Core.Mappings.Abstract;
using Saga.Orchestration.Core.Mappings.Concrete.Mapster;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;

namespace Saga.Orchestration.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisterCoreExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMapping, MapsterMapping>();

            var massTransitSettings = new MassTransitSettings();
            configuration.GetSection(nameof(MassTransitSettings)).Bind(massTransitSettings);
            services.Configure<MassTransitSettings>(configuration.GetSection(nameof(MassTransitSettings)));
            services.AddSingleton(massTransitSettings);

            return services;
        }
    }
}
