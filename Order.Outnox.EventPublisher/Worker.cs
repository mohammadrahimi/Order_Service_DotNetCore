using Framework.RabbitMQ.Setting;
using Microsoft.Extensions.Hosting;
using Order.OutboxPublisher.Publish;
using System.Timers;

namespace Order.Outnox.EventPublisher
{
    public class Worker : BackgroundService
    {
        private readonly OutBoxManagerPublish _outBoxManagerPublish;

        public Worker(OutBoxManagerPublish outBoxManagerPublish)
        {
            _outBoxManagerPublish = outBoxManagerPublish;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(CheckOutBox);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

            return Task.CompletedTask;
        }
        private async void CheckOutBox(object? sender, ElapsedEventArgs e)
        {

            RabbitMQSetting rabbitMQSetting = new(
                      "localhost",
                      "orderVirtual",
                      "eshop",
                      "6661",
                      "application/json");

            await _outBoxManagerPublish.PublishOutBox(rabbitMQSetting);
        }
    }
}
