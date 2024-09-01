

using ErrorOr;
using Order.Domain.Contract.Dto.OrderItem;
using Order.Domain.Order.Errors;
using Order.Domain.Order.ValueObjects;
using Order.Framework.Core.Domain;


namespace Order.Domain.Order.Entites;

public class OrderItem : Entity<OrderItemId>
{
    private OrderItem() : base(OrderItemId.CreateUnique()) { }

    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Price Price { get; private set; }


    private OrderItem(OrderItemId orderItemId, ProductId productId, int quantity, Price price)
           : base(orderItemId)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static ErrorOr<OrderItem> Create(OrderItemDto orderItemDto)
    {
        var _price = Price.Create(orderItemDto.amount, orderItemDto.currency);
        var _productId = ProductId.Create(Guid.Parse(orderItemDto.productId));

        if (_price.IsError)
            return _price.FirstError;
        if (_productId is null)
            return DomainErrors.OrderItem.ProductIdIncorrect;
        if (orderItemDto.quantity <= 0)
            return DomainErrors.OrderItem.QuantityIncorrect;

        var orderItem = new OrderItem(
                 OrderItemId.CreateUnique(),
                 _productId,
                 orderItemDto.quantity,
                 _price.Value
             );

        return orderItem;
    }
}
