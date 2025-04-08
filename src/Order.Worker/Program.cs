using Orders.Api.Features.ShippingToSupplier;
using Orders.Worker.Features.OrderCancel;
using Orders.Worker.Shared.Configuration;
using Orders.Worker.Shared.Infrastructure.Repoistories.MongoDb;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<IShippingToSupplierFeature, ShippingToSupplierFeature>();
builder.Services.AddScoped<IOrderCancelFeature, OrderCancelFeature>();

builder.Services.AddBrokerConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);

var host = builder.Build();
host.Run();
