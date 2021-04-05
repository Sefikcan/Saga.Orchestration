using MediatR;
using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Core.Mappings.Abstract;
using Stock.Infrastructure.DataAccess.EntityFramework;
using Stock.Services.DTO.Request;
using Stock.Services.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Services.Commands
{
    public class IncreaseStockCommand : IRequest<IncreaseStockResponseModel>
    {
        public IncreaseStockRequestModel IncreaseStockRequest { get; set; }

        public IncreaseStockCommand(IncreaseStockRequestModel increaseStockRequest)
        {
            IncreaseStockRequest = increaseStockRequest;
        }
    }

    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, IncreaseStockResponseModel>
    {
        private readonly StockDbContext _dbContext;
        private readonly IMapping _mapping;

        public IncreaseStockCommandHandler(StockDbContext dbContext,
            IMapping mapping)
        {
            _dbContext = dbContext;
            _mapping = mapping;
        }

        public async Task<IncreaseStockResponseModel> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.IncreaseStockRequest.ProductId);
            if (stock == null)
                throw new System.Exception("Product not found!");

            stock.Quantity += request.IncreaseStockRequest.Quantity;
            _dbContext.Stocks.Attach(stock);
            _dbContext.Entry(stock).Property(x => x.Quantity).IsModified = true;
            await _dbContext.SaveChangesAsync();

            return _mapping.Map<Infrastructure.Entities.Stock, IncreaseStockResponseModel>(stock);
        }
    }
}
