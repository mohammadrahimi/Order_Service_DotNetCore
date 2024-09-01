

namespace Framework.RabbitMQ.Setting;

public record RabbitMQSetting(
    string Host,
    string VirtualHost,
    string UserName,
    string Password,
    string JsonMimeType);

