 

using Order.Framework.Core.Domain;

namespace Order.Domain.OutBox.ValueObjects;

public sealed class EventID : ValueObject
{
    public Guid Value { get; }

    //private EventID() { }
    private EventID(Guid value)
    {
        Value = value;
    }
    public static EventID Create(Guid value)
    {
        return new EventID(value);
    }
    public static EventID CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
