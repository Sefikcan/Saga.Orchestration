using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Orchestration.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit;
using Saga.Orchestration.Core.Settings.Concrete.Databases;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment;
using Shipment.Consumer.Consumers;
using Shipment.Infrastructure.DataAccess.EntityFramework;
using System;

namespace Shipment.Consumer
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

                    ShipmentDbSettings shipmentDbSettings = new ShipmentDbSettings();
                    hostContext.Configuration.GetSection(nameof(ShipmentDbSettings)).Bind(shipmentDbSettings);
                    services.AddSingleton(shipmentDbSettings);

                    services.AddDbContext<ShipmentDbContext>(c =>
                        c.UseSqlServer(shipmentDbSettings.ConnectionStrings), ServiceLifetime.Transient);

                    var bus = BusConfigurator.Instance
                    .ConfigureBus(massTransitSettings, (cfg) =>
                    {
                        cfg.ReceiveEndpoint(nameof(CreateShipmentEventModel), e =>
                        {
                            var context = services.BuildServiceProvider().GetService<ShipmentDbContext>();
                            e.Consumer(()=> new CreateShipmentConsumer(context));
                        });
                    });

                    bus.StartAsync();

                    Console.WriteLine("Listening create shipment event..");
                    Console.ReadLine();
                });
        }
    }
}
