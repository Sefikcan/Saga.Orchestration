using Jaeger;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using Saga.Orchestration.Core.Settings.Concrete.Monitoring;

namespace Saga.Orchestration.Core.Extensions
{
    /// <summary>
    /// Jaeger Extensions
    /// </summary>
    public static class JaegerExtensions
    {
        /// <summary>
        /// Add Jaeger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJaeger(this IServiceCollection services, IConfiguration configuration)
        {
            var jaegerSettings = new JaegerSettings();
            configuration.GetSection(nameof(JaegerSettings)).Bind(jaegerSettings);
            services.Configure<JaegerSettings>(configuration.GetSection(nameof(JaegerSettings)));

            //Register OpenTracing and automatically generate spans
            services.AddOpenTracing();

            services.AddSingleton<ITracer>(s =>
            {
                var serviceName = jaegerSettings.ApplicationName;
                ILoggerFactory loggerFactory = s.GetService<ILoggerFactory>();

                // This is necessary to pick the correct sender, otherwise a NoopSender is used!
                Configuration.SenderConfiguration.DefaultSenderResolver = new Jaeger.Senders.SenderResolver(loggerFactory)
                .RegisterSenderFactory<ThriftSenderFactory>();

                var tracer = new Tracer.Builder(serviceName)
                .WithLoggerFactory(loggerFactory)
                .Build();

                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });

            services.Configure<AspNetCoreDiagnosticOptions>(opt =>
            {
                opt.Hosting.IgnorePatterns.Add(ctx => ctx.Request.Path.Value.StartsWith("/status"));
                opt.Hosting.IgnorePatterns.Add(ctx => ctx.Request.Path.Value.StartsWith("/metrics"));
                opt.Hosting.IgnorePatterns.Add(ctx => ctx.Request.Path.Value.StartsWith("/swagger"));
            });

            return services;
        }
    }
}
