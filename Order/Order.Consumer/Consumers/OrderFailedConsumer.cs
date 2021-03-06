using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.DataAccess.EntityFramework;
using Saga.Orchestration.Core.Enums;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using System.Threading.Tasks;

namespace Order.Consumer.Consumers
{
    public class OrderFailedConsumer : IConsumer<IOrderFailedEventModel>
    {
        private readonly OrderDbContext _dbContext;

        public OrderFailedConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<IOrderFailedEventModel> context)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == context.Message.OrderId);
            if (order == null)
                throw new System.Exception("Order not found!");

            order.OrderStatus = (int)OrderStatus.Failed;
            await _dbContext.SaveChangesAsync();
        }
    }
}
