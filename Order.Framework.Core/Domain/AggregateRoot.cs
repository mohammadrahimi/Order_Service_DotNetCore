

using Order.Framework.Core.Bus;
using System.Collections.Generic;

namespace Order.Framework.Core.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : notnull
{

    // [TimeStamp]
    // public byte[] Version { get; set; }
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    protected void AddEventChanges(IDomainEvent @event) =>
        _domainEvents.Add(@event);

    public IReadOnlyList<IDomainEvent> GetChangesDomainEvents() => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();



}
