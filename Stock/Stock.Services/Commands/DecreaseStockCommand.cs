using MediatR;
using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Core.Mappings.Abstract;
using Stock.Infrastructure.DataAccess.EntityFramework;
using Stock.Services.DTO.Request;
using Stock.Services.DTO.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Services.Commands
{
    public class DecreaseStockCommand : IRequest<DecreaseStockResponseModel>
    {
        public DecreaseStockRequestModel DecreaseStockRequest { get; set; }

        public DecreaseStockCommand(DecreaseStockRequestModel decreaseStockRequest)
        {
            DecreaseStockRequest = decreaseStockRequest;
        }
    }

    public class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand, DecreaseStockResponseModel>
    {
        private readonly StockDbContext _dbContext;
        private readonly IMapping _mapping;

        public DecreaseStockCommandHandler(StockDbContext dbContext,
            IMapping mapping)
        {
            _dbContext = dbContext;
            _mapping = mapping;
        }

        public async Task<DecreaseStockResponseModel> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.DecreaseStockRequest.ProductId);
            if (stock == null)
                throw new Exception("Product not found!");

            stock.Quantity -= request.DecreaseStockRequest.Quantity;
            if (stock.Quantity < 0)
                throw new Exception("Quantity must be greater than or equal to zero!");

            _dbContext.Stocks.Attach(stock);
            _dbContext.Entry(stock).Property(x => x.Quantity).IsModified = true;
            await _dbContext.SaveChangesAsync();

            return _mapping.Map<Infrastructure.Entities.Stock, DecreaseStockResponseModel>(stock);
        }
    }
}
