


using Order.Framework.Core.Domain;
 
namespace Order.Domain.Order.ValueObjects;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; }

    //private OrderId() { }
    private OrderId(Guid value)
    {
        Value = value;
    }
    public static OrderId Create(Guid value)
    {
        return new OrderId(value);
    }
    public static OrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
