


using Microsoft.Extensions.DependencyInjection;

namespace Framework.RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        return services;
    }
}
