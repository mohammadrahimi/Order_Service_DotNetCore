


using Order.Framework.Core.Domain;
 

namespace Order.Domain.Order.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; }

    //private ProductId() { }
    private ProductId(Guid value)
    {
        Value = value;
    }
    public static ProductId Create(Guid value)
    {
        return new ProductId(value);
    }
    public static ProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
