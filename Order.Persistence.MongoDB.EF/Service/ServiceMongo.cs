

 
using Order.Persistence.MongoDB.EF.Dto;
using Order.Persistence.MongoDB.EF.Model;
using Order.Persistence.MongoDB.EF.Repository;

namespace Order.Persistence.MongoDB.EF.Service;

public class ServiceMongo
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public ServiceMongo(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }


    public async Task Insert(OrderDto dto)
    {
        var ordermodel = new OrderModel()
        {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            CustomerMobile = dto.CustomerMobile,
            CustomerAddress = dto.CustomerAddress,
            OrderDateTime = dto.OrderDateTime,
            OrderId = dto.OrderId,
            OrderItems = dto.OrderItems,
            OrderState = dto.OrderState,
            PaymentCode = "",
            PaymentPrice = "",
        };
        await _orderMongoRepository.Insert(ordermodel);
    }

    public async Task<OrderModel> ByOrderId(string orderId)
    {
        return await _orderMongoRepository.ById(orderId);
    }
} 
