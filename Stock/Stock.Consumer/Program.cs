using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Orchestration.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit;
using Saga.Orchestration.Core.Settings.Concrete.Databases;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Stock;
using Stock.Consumer.Consumers;
using Stock.Infrastructure.DataAccess.EntityFramework;
using System;

namespace Stock.Consumer
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

                    StockDbSettings stockDbSettings = new StockDbSettings();
                    hostContext.Configuration.GetSection(nameof(StockDbSettings)).Bind(stockDbSettings);
                    services.AddSingleton(stockDbSettings);

                    services.AddDbContext<StockDbContext>(c =>
                        c.UseSqlServer(stockDbSettings.ConnectionStrings), ServiceLifetime.Transient);

                    var bus = BusConfigurator.Instance
                    .ConfigureBus(massTransitSettings, (cfg) =>
                    {
                        cfg.ReceiveEndpoint(nameof(UpdateStockEventModel), e =>
                        {
                            var context = services.BuildServiceProvider().GetService<StockDbContext>();
                            e.Consumer(() => new UpdateStockConsumer(context));
                        });
                    });

                    bus.StartAsync();

                    Console.WriteLine("Listening update stock event..");
                    Console.ReadLine();
                });
        }
    }
}
