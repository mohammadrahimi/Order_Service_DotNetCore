


using Order.Framework.Core.Domain;
 
namespace Order.Domain.Order.ValueObjects;

public sealed class CustomerID : ValueObject
{
    public Guid Value { get; }

    //private CustomerID() { }
    private CustomerID(Guid value)
    {
        Value = value;
    }
    public static CustomerID Create(Guid value)
    {
        return new CustomerID(value);
    }
    public static CustomerID CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
