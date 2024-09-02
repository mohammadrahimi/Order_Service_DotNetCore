

using Order.OutboxPublisher.Dapper;
using Order.OutboxPublisher.Model;

using MassTransit.Initializers;
using Newtonsoft.Json;
using System.Data;
using Framework.RabbitMQ.Publish;
using Framework.RabbitMQ.Setting;

namespace Order.OutboxPublisher.Publish;

public class OutBoxManagerPublish
{
    private readonly IDbConnection _dbConnection;
    private readonly PublishMessage _publishMessage;

    public OutBoxManagerPublish(IDbConnection dbConnection, PublishMessage publishMessage)
    {
        _dbConnection = dbConnection;
        _publishMessage = publishMessage;
    }


    public async Task PublishOutBox(RabbitMQSetting rabbitMQSetting)
    {
        var listNotPublishOutbox = await _dbConnection.GetOutBoxes();
        if (listNotPublishOutbox is not null)
        {
            _publishMessage.SetRabbitMQSetting(rabbitMQSetting);

            foreach (OutBox item in listNotPublishOutbox)
            {
                await _publishMessage.Publish(item.EventType, item.EventBody);
                if (item.EventType == "OrderCreatedEvent")
                {
                    await _publishMessage.Publish("inventoryOrder", item.EventBody);
                }

            }
            _dbConnection.UpdatePublishedDate(listNotPublishOutbox.Select(x => x.Id));
        }

    }
}
