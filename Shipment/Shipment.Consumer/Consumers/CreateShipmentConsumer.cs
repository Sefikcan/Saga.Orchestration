using MassTransit;
using Saga.Orchestration.Core.Enums;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment;
using Shipment.Infrastructure.DataAccess.EntityFramework;
using System.Threading.Tasks;

namespace Shipment.Consumer.Consumers
{
    public class CreateShipmentConsumer : IConsumer<CreateShipmentEventModel>
    {
        private readonly ShipmentDbContext _dbContext;

        public CreateShipmentConsumer(ShipmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateShipmentEventModel> context)
        {
            //Failed işlemi için örnek bir senaryo
            if (context.Message.ShipmentType == (int)ShipmentType.MNG || context.Message.ShipmentType == (int)ShipmentType.Yurtici)
            {
                await context.Publish(new OrderCompletedEventModel
                {
                    OrderId = context.Message.OrderId
                });
            }
            else
            {
                await context.Publish(new OrderFailedEventModel
                {
                    OrderId = context.Message.OrderId
                });

                //TODO: Add StockDecrease event publish
            }

            var shipment = new Infrastructure.Entities.Shipment
            {
                OrderId = context.Message.OrderId,
                ShipmentType = context.Message.ShipmentType
            };

            await _dbContext.AddAsync(shipment);
            if (await _dbContext.SaveChangesAsync()<=0)
            {
                await context.Publish(new OrderFailedEventModel
                {
                    OrderId = context.Message.OrderId
                });

                //TODO: Add StockDecrease event publish
            }
        }
    }
}
