using MassTransit;
using MassTransit.Saga;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Saga.Consumer.Concrete;
using Saga.Orchestration.Core.Constants;
using Saga.Orchestration.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;

namespace Order.Saga.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Db ile ilgili de denenecek!!!
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

                    var orderSaga = new OrderSagaState();
                    var repo = new InMemorySagaRepository<OrderSagaModel>();

                    var bus = BusConfigurator.Instance
                    .ConfigureBus(massTransitSettings, (cfg) =>
                    {
                        cfg.ReceiveEndpoint(BaseConstants.SAGAQUEUENAME, e =>
                        {
                            e.StateMachineSaga(orderSaga, repo);
                        });
                    });

                    bus.StartAsync();

                    Console.WriteLine("Order saga started..");
                    Console.ReadLine();
                });
        }
    }
}
