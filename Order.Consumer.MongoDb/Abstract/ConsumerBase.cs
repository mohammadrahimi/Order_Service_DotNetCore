

using Newtonsoft.Json;
using Order.Consumer.RabbitMQ.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Order.Consumer.RabbitMQ.Abstract;

public class ConsumerBase : RabbitMqClientBase, IConsumerBase
{
    public ConsumerBase(RabbitMqConfiguration config)
        : base(config)
    {
    }

    public async Task Consume<T>(Action<T> onMessage) where T : class
    {
        Channel!.QueueDeclare(queue: typeof(T).Name,
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);
        Channel.BasicQos(0, 1, false);

        var consumer = new AsyncEventingBasicConsumer(Channel);
        consumer.Received += async (ch, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonConvert.DeserializeObject<T>(body);

            Channel.BasicAck(ea.DeliveryTag, false);

            onMessage(message!);

            await Task.Yield();

        };
        Channel.BasicConsume(typeof(T).Name, false, consumer);
        await Task.Yield();
    }


}


