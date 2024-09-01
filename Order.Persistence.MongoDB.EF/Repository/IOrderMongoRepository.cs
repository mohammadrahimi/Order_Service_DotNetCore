
 
using Order.Persistence.MongoDB.EF.Dto;
using Order.Persistence.MongoDB.EF.Model;

namespace Order.Persistence.MongoDB.EF.Repository;

public interface IOrderMongoRepository
{
    Task Insert(OrderModel orderModel);
    Task UpdateCustomer(OrderDto dto);
    Task UpdatePayMent(PayMentDto dto);
    Task UpdateOrder(OrderDto dto);
    Task Delete(OrderModel orderModel);

    Task<OrderModel> ById(string orderId);
    Task<List<OrderModel>> All();
}
