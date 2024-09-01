

namespace Order.Consumer.RabbitMQ.Abstract;

public interface IConsumerBase
{
    Task Consume<T>(Action<T> onMessage) where T : class;

}
