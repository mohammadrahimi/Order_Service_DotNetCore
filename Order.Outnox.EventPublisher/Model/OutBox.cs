
 

namespace Order.OutboxPublisher.Model;

public record OutBox(
          long Id,
          string EventType,
          string EventBody,
          Guid EventId,
          DateTime? publishedAt,
          DateTime CreateDateTime
);

