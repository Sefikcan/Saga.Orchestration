using MassTransit;
using MediatR;
using Order.Infrastructure.DataAccess.EntityFramework;
using Order.Services.DTO.Request;
using Order.Services.DTO.Response;
using Saga.Orchestration.Core.Constants;
using Saga.Orchestration.Core.Enums;
using Saga.Orchestration.Core.Mappings.Abstract;
using Saga.Orchestration.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit;
using Saga.Orchestration.Core.Settings.Concrete.MessageBrokers;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Services.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderResponseModel>
    {
        public CreateOrderRequestModel CreateOrderRequest { get; set; }

        public CreateOrderCommand(CreateOrderRequestModel createOrderRequest)
        {
            CreateOrderRequest = createOrderRequest;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponseModel>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapping _mapping;
        private readonly ISendEndpoint _sendEndpoint;
        private readonly MassTransitSettings _massTransitSettings;

        public CreateOrderCommandHandler(OrderDbContext dbContext,
            IMapping mapping,
            MassTransitSettings massTransitSettings)
        {
            _dbContext = dbContext;
            _mapping = mapping;
            _massTransitSettings = massTransitSettings;

            var busInstance = BusConfigurator.Instance.ConfigureBus(_massTransitSettings);
            _sendEndpoint = busInstance.GetSendEndpoint(new Uri($"{_massTransitSettings.Uri}/{BaseConstants.SAGAQUEUENAME}")).Result;
        }

        public async Task<CreateOrderResponseModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapping.Map<CreateOrderRequestModel, Infrastructure.Entities.Order>(request.CreateOrderRequest);
            order.OrderStatus = (int)OrderStatus.Pending;

            var response = await _dbContext.AddAsync(order);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                await _sendEndpoint.Send(new OrderSagaEventModel
                {
                    OrderId = order.Id,
                    Quantity = request.CreateOrderRequest.Quantity,
                    ProductId = request.CreateOrderRequest.ProductId
                });
            }
            else
            {
                await _sendEndpoint.Send(new OrderFailedEventModel
                {
                    OrderId = order.Id
                });
            }

            return _mapping.Map<Infrastructure.Entities.Order, CreateOrderResponseModel>(response.Entity);
        }
    }
}
