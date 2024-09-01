


using Order.Consumer.RabbitMQ.Model;
using RabbitMQ.Client;

namespace Order.Consumer.RabbitMQ.Abstract;

public abstract class RabbitMqClientBase : IDisposable
{
    protected IModel? Channel { get; private set; }
    private IConnection? _connection;

    public RabbitMqClientBase(RabbitMqConfiguration config)
    {
        ConnectToRabbitMq(config);
    }

    private void ConnectToRabbitMq(RabbitMqConfiguration config)
    {
        if (_connection == null || _connection.IsOpen == false)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = config.Username,   
                Password = config.Password, 
                HostName = config.Host,  
                VirtualHost = config.VirtualHost,  
            };
            factory.DispatchConsumersAsync = true;

            _connection = factory.CreateConnection();
        }

        if (Channel == null || Channel.IsOpen == false)
        {
            Channel = _connection.CreateModel();
        }
    }
    public void Dispose()
    {
        if (Channel!.IsOpen)
        {
            Channel.Close();
            Channel?.Dispose();
            Channel = null;
        }

        if (_connection!.IsOpen)
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }

    }
}
