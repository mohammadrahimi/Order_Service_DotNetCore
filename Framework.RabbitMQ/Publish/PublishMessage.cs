
using Framework.RabbitMQ.Setting;
using RabbitMQ.Client;
using System.Text;


namespace Framework.RabbitMQ.Publish;

public sealed class PublishMessage
{
    private RabbitMQSetting? _rabbitMQSetting;
    private IModel? _chanel;
    public PublishMessage()
    {
    }
    public void SetRabbitMQSetting(RabbitMQSetting rabbitMQSetting)
    {
        _rabbitMQSetting = rabbitMQSetting;
    }
    public Task Publish(string queue, string message)
    {
        var _connection = CreateConnection();
        if (_connection is null) return Task.CompletedTask;

        _chanel = _connection.CreateModel();

        _chanel.QueueDeclare(
            queue: queue,
            false, false, false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        var messageProperties = _chanel.CreateBasicProperties();
        messageProperties.ContentType = _rabbitMQSetting!.JsonMimeType;

        _chanel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: messageProperties,
            body: body);

        return Task.CompletedTask;

    }
    private IConnection? CreateConnection()
    {
        try
        {
            var _factory = new ConnectionFactory
            {
                HostName = _rabbitMQSetting!.Host,
                VirtualHost = _rabbitMQSetting!.VirtualHost,
                UserName = _rabbitMQSetting!.UserName,
                Password = _rabbitMQSetting!.Password
            };

            return _factory.CreateConnection();
        }
        catch (Exception)
        {
            // log   Exception
        }
        return null;
    }
}
