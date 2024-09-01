
using Microsoft.Data.SqlClient;
using System.Data;
using Order.OutboxPublisher.Publish;
using Framework.RabbitMQ.Publish;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Order.Outnox.EventPublisher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IDbConnection>(new SqlConnection(hostContext.Configuration.GetConnectionString("OutBox")));
        services.AddSingleton<OutBoxManagerPublish>();
         services.AddSingleton<PublishMessage>();

        services.AddHostedService<Worker>();
    })

    .Build();

host.Run();
