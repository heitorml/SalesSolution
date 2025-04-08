using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Features.OrdersResale;
using Orders.Api.Features.OrdersSupplier;
using Orders.Api.Shared.Configuration;
using Orders.Api.Shared.Responses;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Pedidos API",
        Version = "v1",
        Description = "API para gerenciamento de pedidos e envio para fornecedores",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

builder.Services.AddBrokerConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFeatures();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
    });
}

app.UseHttpsRedirection();

var group = app.MapGroup("/orders").WithTags("Orders");
CreateOrdersResalesEndpoint.MapCreateOrdersResaleEndpoints(group);
CreateOrderSupplierEndpoint.MapCreateOrdersSupplierEndpoints(group);

app.Run();
