


using ErrorOr;
using FluentValidation;
using Order.Domain.Contract.Commands.Order.Create;
using Order.Domain.Contract.Dto.OrderItem;
using Order.Domain.Contract.Event.Order;
using Order.Domain.Order.Entites;
using Order.Domain.Order.Errors;
using Order.Domain.Order.State;
using Order.Domain.Order.ValueObjects;
using Order.Framework.Core.Domain;


namespace Order.Domain.Order;

public sealed class Order : AggregateRoot<OrderId>
{
    private Order() : base(OrderId.CreateUnique()) { }

    public CustomerID CustomerID { get; private set; }
    public string CustomerName { get; private set; }
    public DateTime OrderDateTime { get; private set; }
    public OrderState State { get; private set; }
    public Price TotalPrice { get; private set; }

    private readonly static List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    private Order(OrderId orderId, CustomerID customerID, string customerName, DateTime orderDateTime, Price totalPrice)
         : base(orderId)
    {
        CustomerID = customerID;
        CustomerName = customerName;
        OrderDateTime = orderDateTime;
        TotalPrice = totalPrice;
        State = new PendingState();
    }

    public static ErrorOr<Order> Create(CreateOrderCommand command)
    {

        var _customerID = CustomerID.Create(Guid.Parse(command.customerID));
        var _totalPrice = Price.Create(20000, "rial");

        if (_customerID is null)
            return DomainErrors.Order.CustomerIDIncorrect;

        if (_totalPrice.IsError)
            return _totalPrice.FirstError;

        command.orderItemsDto.ForEach(_orderItemDto =>
        {
            AddOrderItem(_orderItemDto.productId, _orderItemDto.quantity, _orderItemDto.amount, _orderItemDto.currency);
        });


        var order = new Order(
            OrderId.CreateUnique(),
            _customerID,
            command.customerName,
            DateTime.UtcNow,
            _totalPrice.Value);

        order.AddEventChanges(new OrderCreatedEvent(
                 order.Id.Value.ToString(),
                _customerID.Value.ToString(),
                command.customerName,
                DateTime.UtcNow.ToString(),
                command.totalAmount,
                command.totalCurrency,
                command.orderItemsDto,
                order.State.ToString()
            ));

        return order;
    }

    public static ErrorOr<Success> AddOrderItem(string productId, int quantity, float amount, string currency)
    {
        var dto = new OrderItemDto(productId, quantity, amount, currency);
        var orderItem = OrderItem.Create(dto);
        if (orderItem.IsError)
            return orderItem.FirstError;

        _orderItems.Add(orderItem.Value);

        return Result.Success;
    }
    public static ErrorOr<Success> RemoveItem(string orderItemId)
    {
        var _orderItemId = OrderItemId.Create(Guid.Parse(orderItemId));

        if (_orderItemId is null)
            return DomainErrors.Order.OrderItemIdIncorrect;

        _orderItems.RemoveAll(x => x.Id == _orderItemId);

        return Result.Success;
    }

    public void Confirm()
    {
        State = State.Confirmed();
        //AddEventChanges(new OrderConfirmedEvent(Id));
    }

    public void Reject()
    {
        State = State.Reject();
        //AddEventChanges(new OrderRejectedEvent(Id));
    }

}

