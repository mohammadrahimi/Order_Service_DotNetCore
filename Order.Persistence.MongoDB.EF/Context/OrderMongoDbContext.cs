



using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Order.Persistence.MongoDB.EF.Model;
 
namespace Order.Persistence.MongoDB.EF.Context;

public class OrderMongoDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions)
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017/OrderDB");
        dbContextOptions.UseMongoDB(mongoClient, "OrderDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderModel>().ToCollection("OrdersModel");
    }
    public DbSet<OrderModel> OrdersModel { get; init; }

}
