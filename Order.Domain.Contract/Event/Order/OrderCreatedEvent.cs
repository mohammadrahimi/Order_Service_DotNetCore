
using Order.Domain.Contract.Dto.OrderItem;
using Order.Framework.Core.Bus;
using System;
using System.Collections.Generic;

namespace Order.Domain.Contract.Event.Order;

public class OrderCreatedEvent : IDomainEvent
{
    public OrderCreatedEvent(string orderId,
        string customerId,
        string customerName,
        string orderDateTime,
        float totalAmount, string totalCurrency,
        List<OrderItemDto> items,
        string state)
    {
        OrderId = orderId;
        CustomerId = customerId;
        CustomerName = customerName;
        OrderDateTime = orderDateTime;
        TotalAmount = totalAmount;
        TotalCurrency = totalCurrency;
        OrderItems = items;
        State = state;
    }

    public string OrderId { get; private set; }
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string OrderDateTime { get; private set; }
    public float TotalAmount { get; private set; }
    public string TotalCurrency { get; private set; }
     
    public List<OrderItemDto> OrderItems { get; private set; }
    public string State { get; private set; }
     

}

