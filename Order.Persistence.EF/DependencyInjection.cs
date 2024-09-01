using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Order.Repository;
using Order.Persistence.EF.Context;
using Order.Persistence.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace Order.Persistence.EF;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
          .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<OrderDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("OrderConnection")));

        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }

}
