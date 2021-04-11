using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.DataAccess.EntityFramework;
using Saga.Orchestration.Core.Enums;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using System.Threading.Tasks;

namespace Order.Consumer.Consumers
{
    public class OrderCompletedConsumer : IConsumer<IOrderCompletedEventModel>
    {
        private readonly OrderDbContext _dbContext;

        public OrderCompletedConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<IOrderCompletedEventModel> context)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == context.Message.OrderId);
            if (order == null)
            {
                await context.Publish<IOrderFailedEventModel>(new OrderFailedEventModel
                {
                    OrderId = order.Id
                });

                // TODO: Update stock event model added

                throw new System.Exception("Order not found!");
            }

            order.OrderStatus = (int)OrderStatus.Success;
            await _dbContext.SaveChangesAsync();
        }
    }
}
