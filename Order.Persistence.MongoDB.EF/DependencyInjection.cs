 
using Microsoft.Extensions.DependencyInjection;
using Order.Persistence.EF.Repository;
using Order.Persistence.MongoDB.EF.Repository;

namespace Order.Persistence.MongoDB.EF;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceMongoDB(
        this IServiceCollection services
       )
    {
        services.AddScoped<IOrderMongoRepository,  OrderMongoRepository>();
        return services;
    }
     

}
