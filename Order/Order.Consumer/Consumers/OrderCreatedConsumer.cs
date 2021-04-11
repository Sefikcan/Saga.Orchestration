using MassTransit;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using System;
using System.Threading.Tasks;

namespace Order.Consumer.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreatedEventModel>
    {
        public async Task Consume(ConsumeContext<IOrderCreatedEventModel> context)
        {
            await context.Publish<IUpdateStockEventModel>(new 
            {
                CorrelationId = context.CorrelationId ?? Guid.Empty,
                context.Message.OrderId,
                context.Message.ProductId,
                context.Message.Quantity
            });
        }
    }
}
