using Application.UseCases.Orders.ShippingToSupplier;
using Infrastructure;
using Infrastructure.Repoistories.MongoDb;
using Orders.Worker.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<IShippingToSupplierUseCase, ShippingToSupplierUseCase>();

builder.Services.AddBrokerConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);

var host = builder.Build();
host.Run();
