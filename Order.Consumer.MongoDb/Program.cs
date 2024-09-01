using System.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Order.Consumer.RabbitMQ.Model;
using Order.Consumer.RabbitMQ.Abstract;
using Order.Consumer.RabbitMQ.Consumer;
using Order.Persistence.MongoDB.EF.Service;

IHost host = Host.CreateDefaultBuilder(args)
     .ConfigureServices((hostContext, services) =>
     {
         IConfiguration configuration = hostContext.Configuration;

         var config = new RabbitMqConfiguration();
         config.Host =  configuration.GetSection("RabbitMqConfiguration").GetValue<string>("Host")!;
         config.VirtualHost = configuration.GetSection("RabbitMqConfiguration").GetValue<string>("VirtualHost")!;
         config.Username = configuration.GetSection("RabbitMqConfiguration").GetValue<string>("Username")!;
         config.Password = configuration.GetSection("RabbitMqConfiguration").GetValue<string>("Password")!;

         services.AddSingleton<ServiceMongo>();
         services.AddSingleton<RabbitMqConfiguration>(config);
         services.AddSingleton<IConsumerBase, ConsumerBase>();
         services.AddHostedService<OrderCreatedEventConsumer>();

     })
    .Build();

host.Run();
