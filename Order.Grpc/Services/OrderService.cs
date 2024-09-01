using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Order.Persistence.MongoDB.EF.Repository;

namespace Order.Grpc.Services;

public class OrderService : OrderQuery.OrderQueryBase
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public OrderService(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public override async Task<OrderResponse> ByOrderID(OrderRequest request, ServerCallContext context)
    {
        var _order = await _orderMongoRepository.ById(request.OrderId);

        return (new OrderResponse
        {
            OrderId = _order.OrderId,
            CustomerName = _order.CustomerName,
            CustomerAddress = _order.CustomerAddress,
            CustomerId = _order.CustomerId,
            CustomerMobile = _order.CustomerMobile,
            OrderDateTime = _order.OrderDateTime,
            OrderItems = _order.OrderItems,
            OrderState = _order.OrderState,
            PaymentCode = _order.PaymentCode,
            PaymentPrice = _order.PaymentPrice,
        });
    }

    public override async Task<ListOrdersResponse> AllOrders(Empty request, ServerCallContext context)
    {
        var _orders = await _orderMongoRepository.All();

        var listOrders = new List<OrderResponse>();
        if (_orders.Count > 0)
        {
            foreach (var order in _orders)
            {
                var itemorder = new OrderResponse()
                {
                    OrderId = order.OrderId,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    CustomerId = order.CustomerId,
                    CustomerMobile = order.CustomerMobile,
                    OrderDateTime = order.OrderDateTime,
                    OrderItems = order.OrderItems,
                    OrderState = order.OrderState,
                    PaymentCode = order.PaymentCode,
                    PaymentPrice = order.PaymentPrice,
                };
                listOrders.Add(itemorder);
            }
        }

        return (new ListOrdersResponse
        {
            ListOrders = { listOrders }
        });
    }

}
