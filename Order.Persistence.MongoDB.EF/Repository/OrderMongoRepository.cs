


using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Order.Persistence.MongoDB.EF.Context;
using Order.Persistence.MongoDB.EF.Dto;
using Order.Persistence.MongoDB.EF.Model;
using Order.Persistence.MongoDB.EF.Repository;

namespace Order.Persistence.EF.Repository;

public class OrderMongoRepository : IOrderMongoRepository
{
    private readonly OrderMongoDbContext _dbContext;

    public OrderMongoRepository(OrderMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Insert(OrderModel orderModel)
    {
        await _dbContext.OrdersModel.AddAsync(orderModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(OrderModel orderModel)
    {
        _dbContext.Remove(orderModel);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdateCustomer(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrder(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePayMent(PayMentDto dto)
    {
        throw new NotImplementedException();
    }


    public Task<List<OrderModel>> All()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderModel> ById(string orderId)
    {
        return await _dbContext.OrdersModel.SingleOrDefaultAsync(x => x.OrderId == orderId);
    }

}
