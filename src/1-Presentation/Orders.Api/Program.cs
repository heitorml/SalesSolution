using Application.UseCases.Orders.OrdersSupplier;
using Application.UseCases.Orders.Receive;
using Infrastructure;
using Infrastructure.Repoistories.MongoDb;
using Orders.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<ICreateOrderResalesUseCase, CreateOrderResalesUseCase>();
builder.Services.AddScoped<ICreateOrderSupplierUseCase, CreateOrderSupplierUseCase>();

builder.Services.AddBrokerConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
