


using Order.Framework.Core.Bus;
using System.Collections.Generic;

namespace Order.Framework.Core.Domain;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> GetChangesDomainEvents();
    void ClearDomainEvents();
}