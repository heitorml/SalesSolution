using Resales.Api.Features.Create;
using Resales.Api.Features.GetAll;
using Resales.Api.Features.GetById;
using Resales.Api.Features.Updade;
using Resales.Api.Shared.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Revenda API",
        Version = "v1",
        Description = "API para gerenciamento de Revendas/parceiros",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resales API V1");
    });
}

app.UseHttpsRedirection();

var group = app.MapGroup("/resales").WithTags("Resales");
ResalesCreateEndpoint.MapCreateResalesEndpoints(group);
UpdateResaleEndpoint.MapUpdateResalesEndpoints(group);
GetResaleByIdEndpoint.MapGetResaleByIdEndpoints(group);
GetAllResaleEndpoint.MapGetAllResaleEndpoints(group);

//app.UseAuthorization();
app.Run();
