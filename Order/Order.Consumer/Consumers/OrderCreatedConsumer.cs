using MassTransit;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Stock;
using System.Threading.Tasks;

namespace Order.Consumer.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreatedEventModel>
    {
        public async Task Consume(ConsumeContext<IOrderCreatedEventModel> context)
        {
            await context.Publish(new UpdateStockEventModel 
            {
                OrderId = context.Message.OrderId,
                ProductId = context.Message.ProductId,
                Quantity = context.Message.Quantity
            });
        }
    }
}
