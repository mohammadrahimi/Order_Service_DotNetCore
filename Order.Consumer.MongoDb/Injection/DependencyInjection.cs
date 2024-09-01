using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Consumer.RabbitMQ.Model;

namespace Order.Consumer.RabbitMQ.Injection;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMQConsume(this IServiceCollection services, IConfiguration configuration)
    {
        services
          .AddRabbitMQ(configuration);

        return services;
    }
    private static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfiguration>(
            a => configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));

        return services;
    }
}
