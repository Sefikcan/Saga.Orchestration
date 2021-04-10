using Automatonymous;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Abstract;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Order;
using Saga.Orchestration.Shared.MessageBrokers.Consumers.Models.Sagas.Order;
using System;

namespace Order.Saga.Consumer.Concrete
{
    public class OrderSagaState : MassTransitStateMachine<OrderSagaModel>
    {
        #region States

        public State Received { get; set; }

        public State Created { get; set; }

        public State ChangeStock { get; set; }

        public State Shipping { get; set; }

        public State Processed { get; set; }

        public State Failed { get; set; }

        #endregion

        #region Events

        public Event<IOrderSagaEventModel> OrderReceived { get; set; }

        public Event<IOrderCreatedEventModel> OrderCreated { get; set; }

        public Event<IUpdateStockEventModel> UpdateStock { get; set; }

        public Event<ICreateShipmentEventModel> CreateShipment { get; set; }

        public Event<IOrderCompletedEventModel> OrderCompleted { get; set; }

        public Event<IOrderFailedEventModel> OrderFailed { get; set; }

        #endregion

        public OrderSagaState()
        {
            InstanceState(c => c.CurrentState); //anlık state bilgisini almak için kullanılır.

            //Event(() => OrderCreated,
            //    c=>c.CorrelateBy(c=>c.OrderCode, ctx=>ctx.Message.OrderCode).SelectId(s=>Guid.NewGuid())); string bir alana sahipsek ve akış takibini yapmak istersek CorrelateBy ile id oluşturup set ediyoruz. Bu id otomatik olarak diğer steplerde kullanmak için set edilir.

            Event(() => OrderReceived,
                c => c.CorrelateById(c => c.OrderId, ctx => ctx.Message.OrderId).SelectId(s => Guid.NewGuid()));
            //int bir alana correlationid set etme işlemini gerçekleştirir.Bu id'de otomatik olarak diğer steplerde kullanılmak üzere otomatik set edilir.

            //Eventlerimze akışta takip edebileceğimiz correlationid bilgisini set ediyoruz.
            Event(() => OrderCreated,
              c => c.CorrelateById(s => s.Message.CorrelationId));

            Event(() => UpdateStock,
                c => c.CorrelateById(s => s.Message.CorrelationId));

            Event(() => CreateShipment,
               c => c.CorrelateById(s => s.Message.CorrelationId));

            Event(() => OrderCompleted,
               c => c.CorrelateById(s => s.Message.CorrelationId));

            Event(() => OrderFailed,
               c => c.CorrelateById(s => s.Message.CorrelationId));

            // İlk başta hangi işlem gerçekleşeceğini tanımlıyoruz.
            Initially(
                When(OrderReceived)
                .Then(ctx =>
                {
                    ctx.Instance.OrderId = ctx.Data.OrderId;
                    ctx.Instance.ProductId = ctx.Data.ProductId;
                    ctx.Instance.Quantity = ctx.Data.Quantity;
                })
                .ThenAsync(
                    ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} is received")
                    )
                .Publish(ctx => new OrderCreatedEventModel(ctx.Instance))
                .TransitionTo(Received)
                );

            During(Received,
                When(OrderCreated)
                .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} order created event triggered."))
                .TransitionTo(Created));

            During(Created,
                When(UpdateStock)
                .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} update stock event triggered."))
                .TransitionTo(ChangeStock)
                .Finalize(),
                When(OrderFailed)
                .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} stock update failed."))
                .TransitionTo(Failed)
                .Finalize());

            // During araya girme işlemini yapar. Burada event tetiklendikten sonra console'a ilgili event'in tetiklendiği bilgisini yazıp, Finalize ile tüm akışı sonlandırıyoruz.

            During(ChangeStock,
               When(CreateShipment)
               .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} create shipping event triggered."))
               .TransitionTo(Shipping)
               .Finalize(),
                When(OrderFailed)
                .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} create shipping failed."))
                .TransitionTo(Failed)
                .Finalize());

            During(Shipping,
               When(OrderCompleted)
               .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} order completed event triggered."))
               .TransitionTo(Processed)
               .Finalize(),
                When(OrderFailed)
                .ThenAsync(ctx => Console.Out.WriteLineAsync($"{ctx.Data.OrderId} create order failed."))
                .TransitionTo(Failed)
                .Finalize());

            SetCompletedWhenFinalized(); // Akışımızdaki tüm işlemlerin bittiğini saga'ya belirtir ve ilgili instance repo üzerinden otomatik olarak silinir.

            //During(Accepted,
            //Ignore(SubmitOrder)); // event'i ignore etmek için kullanırız

            //CompositeEvent(() => OrderReady, x => x.ReadyEventStatus, SubmitOrder, OrderAccepted);
            //akıştaki ilgili event'ler (submit order ve orderaccepted) tamamlandıktan sonra çalışır(order ready event'i)

            //Event(() => OrderCancellationRequested, e =>
            //{
            //    e.CorrelateById(context => context.Message.OrderId);

            //    e.OnMissingInstance(m =>
            //    {
            //        return m.ExecuteAsync(x => x.RespondAsync<OrderNotFound>(new { x.OrderId }));
            //    });
            //}); -> event'imiz herhangi bir event ile eşleşmiyorsa bu şekilde bir yapı ile bilgi sahibi olabilirsin
        }
    }
}
