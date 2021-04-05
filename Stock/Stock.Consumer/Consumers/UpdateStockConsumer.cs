using MassTransit;
using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Core.Enums;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Stock;
using Stock.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Stock.Consumer.Consumers
{
    public class UpdateStockConsumer : IConsumer<UpdateStockEventModel>
    {
        private readonly StockDbContext _dbContext;

        public UpdateStockConsumer(StockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UpdateStockEventModel> context)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == context.Message.ProductId);
            if (stock == null)
            {
                await context.Publish(new OrderFailedEventModel
                {
                    OrderId = context.Message.OrderId
                });

                throw new Exception("Product not found!");
            }

            stock.Quantity -= context.Message.Quantity;
            if (stock.Quantity < 0)
            {
                await context.Publish(new OrderFailedEventModel
                {
                    OrderId = context.Message.OrderId
                });

                throw new Exception("Quantity must be greater than or equal to zero!");
            }

            _dbContext.Stocks.Attach(stock);
            _dbContext.Entry(stock).Property(x => x.Quantity).IsModified = true;
            if (await _dbContext.SaveChangesAsync()>0)
            {
                await context.Publish(new CreateShipmentEventModel
                {
                    OrderId = context.Message.OrderId,
                    ShipmentType = (int)ShipmentType.MNG
                });
            }
            else
            {
                await context.Publish(new OrderFailedEventModel
                {
                    OrderId = context.Message.OrderId
                });
            }
        }
    }
}
