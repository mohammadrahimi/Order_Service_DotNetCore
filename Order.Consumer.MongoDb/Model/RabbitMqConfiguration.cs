

namespace Order.Consumer.RabbitMQ.Model;

public class RabbitMqConfiguration
{
    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
