using Automatonymous;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Shipment;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Consumer.OrderSaga
{
    public class OrderSagaState : MassTransitStateMachine<OrderSagaModel>
    {
        public State Pending { get; set; }

        public State Completed { get; set; }

        public Event<UpdateStockEventModel> UpdateStockCommand { get; set; }

        public Event<CreateShipmentEventModel> CreateShipmentCommand { get; set; }

        public Event<OrderCreatedEventModel> OrderCreatedCommand { get; set; }


        public OrderSagaState()
        {
            InstanceState(c => c.CurrentState); //Mevcut state bilgisini almak için kullanılır.

            //Event sıralaması önemli!

            Event(()=> UpdateStockCommand , op=>op.CorrelateById(x=>x.OrderId, ctx=> ctx.Message.OrderId).SelectId(s=> Guid.NewGuid()));

            Event(() => CreateShipmentCommand, op=> op.CorrelateById(s=>s.Message.CorrelationId)); // Akış takibini yapabilmek için id oluşturup set ediyoruz. Bu id'de otomatik olarak diğer steplerde kullanılmak üzere otomatik set edilir.

            //Akışı başlatıyoruz.
            Initially(
                When(UpdateStockCommand)
                .Then(ctx=> 
                {
                })
                .ThenAsync(
                    ct=>Console.Out.WriteLineAsync("UpdateStockCommand is started..")
                    )
                .TransitionTo(Pending) //Order'ın status'ünü pending olarak set ediyoruz
                .Publish(ct=> new UpdateStockEventModel(ctx.Instance)));
        }
    }
}
