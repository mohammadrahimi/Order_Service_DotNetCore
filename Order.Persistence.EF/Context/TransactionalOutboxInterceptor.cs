 
 
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Order.Domain.OutBox;
using Order.Framework.Core.Domain;
using System.Collections.ObjectModel;
using System.Threading;

namespace Framework.Core.Persistence;

public sealed class TransactionalOutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
        )
    {
        var dbContext = eventData.Context!;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);


        var eventsOutBox = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
                 {
                     var domainEvents = aggregateRoot.GetChangesDomainEvents().ToList();
                     aggregateRoot.ClearDomainEvents();

                     return domainEvents;
                 })
            .Select(domainEvent => new OutBox(
                domainEvent.GetType().Name!,
                 JsonConvert.SerializeObject(domainEvent),
                 null
               ))
            .ToList();

        dbContext.Set<OutBox>().AddRangeAsync(eventsOutBox);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}