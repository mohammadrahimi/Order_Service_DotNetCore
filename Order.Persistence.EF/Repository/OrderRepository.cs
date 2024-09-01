

 
using Order.Domain.Order.Repository;
using Order.Domain.Order.ValueObjects;
using Order.Persistence.EF.Context;

using Microsoft.EntityFrameworkCore;
 

namespace Order.Persistence.EF.Repository;

public class OrderRepository : IOrderRepository
{

    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Order.Order> ById(Guid Id)
    {
        return await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == OrderId.Create(Id));
    }

    //public async Task<List<Domain.Order.Order>> ByCustomerId(Guid customerId)
    //{
    //    return await _dbContext.Orders.Where(x => x.CustomerID == CustomerID.Create(customerId)).ToListAsync();
    //}

    public async Task Save(Domain.Order.Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Domain.Order.Order order)
    {
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Domain.Order.Order order)
    {
        _dbContext.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}
