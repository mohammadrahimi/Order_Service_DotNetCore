using Order.Grpc.Services;
using Order.Persistence.EF.Repository;
using Order.Persistence.MongoDB.EF.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

builder.Services.AddSingleton<IOrderMongoRepository, OrderMongoRepository>();

var app = builder.Build();
app.MapGrpcService<OrderService>();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
