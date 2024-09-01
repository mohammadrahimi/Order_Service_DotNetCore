


using Order.Framework.Core.Domain;
using System;
using System.Collections.Generic;

namespace Order.Domain.Order.ValueObjects;

public sealed class OrderItemId : ValueObject
{
    public Guid Value { get; }

    //private OrderItemId() { }
    private OrderItemId(Guid value)
    {
        Value = value;
    }
    public static OrderItemId Create(Guid value)
    {
        return new OrderItemId(value);
    }
    public static OrderItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
