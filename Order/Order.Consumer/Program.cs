using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Consumer.Consumers;
using Order.Infrastructure.DataAccess.EntityFramework;
using Saga.Orchestration.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit;
using Saga.Orchestration.Core.Settings.Concrete.Databases;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using System;

namespace Order.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", true, true);
                config.AddEnvironmentVariables();

                if (args != null)
                    config.AddCommandLine(args);
            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();

                    var massTransitSettings = hostContext.Configuration.GetSection("MassTransitSettings")
                        .Get<MassTransitSettings>();
                    services.AddSingleton(massTransitSettings);

                    OrderDbSettings orderDbSettings = new OrderDbSettings();
                    hostContext.Configuration.GetSection(nameof(OrderDbSettings)).Bind(orderDbSettings);
                    services.AddSingleton(orderDbSettings);

                    services.AddDbContext<OrderDbContext>(c =>
                        c.UseSqlServer(orderDbSettings.ConnectionStrings), ServiceLifetime.Transient);

                    var bus = BusConfigurator.Instance
                            .ConfigureBus(massTransitSettings, (cfg) =>
                            {
                                cfg.ReceiveEndpoint(nameof(OrderCompletedEventModel), e =>
                                {
                                    var context = services.BuildServiceProvider().GetService<OrderDbContext>();
                                    e.Consumer(() => new OrderCompletedConsumer(context));
                                });

                                cfg.ReceiveEndpoint(nameof(OrderCreatedEventModel), e =>
                                {
                                    e.Consumer(() => new OrderCreatedConsumer());
                                });

                                cfg.ReceiveEndpoint(nameof(OrderFailedEventModel), e =>
                                {
                                    var context = services.BuildServiceProvider().GetService<OrderDbContext>();
                                    e.Consumer(() => new OrderFailedConsumer(context));
                                });
                            });

                    bus.StartAsync();

                    Console.WriteLine("Listening order completed event..");
                    Console.ReadLine();

                });
        }
    }
}
