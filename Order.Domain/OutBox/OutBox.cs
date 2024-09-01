

using Order.Domain.OutBox.ValueObjects;
 

namespace Order.Domain.OutBox;

public class OutBox
{
    public long Id { get; }
    public string EventType { get; private set; }
    public string EventBody { get; private set; }
    public EventID EventId { get; private set; }
    public DateTime? PublishedAt { get; }
    public DateTime CreateDateTime { get; }

    public OutBox(
           string eventType,
           string eventBody,
           DateTime? publishedAt
         )
    {
        EventType = eventType;
        EventBody = eventBody;
        EventId = EventID.CreateUnique();
        PublishedAt = publishedAt!;
        CreateDateTime = DateTime.UtcNow;
    }


}
