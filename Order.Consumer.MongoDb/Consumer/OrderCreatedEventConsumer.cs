

using Newtonsoft.Json;
using Order.Consumer.RabbitMQ.Abstract;
using Order.Domain.Contract.Event.Order;
using Order.Persistence.MongoDB.EF.Dto;
using Order.Persistence.MongoDB.EF.Service;

namespace Order.Consumer.RabbitMQ.Consumer;

public class OrderCreatedEventConsumer : BackgroundService
{
    private readonly IConsumerBase _consumerBase;
    private readonly ServiceMongo _serviceMongo;

    public OrderCreatedEventConsumer(IConsumerBase consumerBase, ServiceMongo serviceMongo)
    {
        _consumerBase = consumerBase;
        _serviceMongo = serviceMongo;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        await _consumerBase.Consume<OrderCreatedEvent>(x =>
        {
            Task.Run(() => { ConsumeMessage(x); }, stoppingToken);
        });

    }
    private async void ConsumeMessage(OrderCreatedEvent orderCreatedEvent)
    {
        var _orderItems = JsonConvert.SerializeObject(orderCreatedEvent.OrderItems);

        var orderDto = new OrderDto(
           orderCreatedEvent.OrderId,
            _orderItems,
            orderCreatedEvent.OrderDateTime,
            orderCreatedEvent.State,
            orderCreatedEvent.CustomerName,
            orderCreatedEvent.CustomerId,
            "",
            ""
        );

        await _serviceMongo.Insert(orderDto);

        Console.WriteLine(" OrderCreatedEvent ");
    }

}
