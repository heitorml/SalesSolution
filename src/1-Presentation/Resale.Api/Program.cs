using Application.UseCases.Resales.Create;
using Application.UseCases.Resales.GetAll;
using Application.UseCases.Resales.GetByFilter;
using Application.UseCases.Resales.GetById;
using Application.UseCases.Resales.Updade;
using Infrastructure;
using Infrastructure.Repoistories.MongoDb;
using Resale.Api.Configuration;
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
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<IUpdateResaleUseCase, UpdateResaleUseCase>();
builder.Services.AddScoped<IResalesCreateUseCase, ResalesCreateUseCase>();
builder.Services.AddScoped<IGetAllResaleUseCase, GetAllResaleUseCase>();
builder.Services.AddScoped<IGetResaleByIdUseCase, GetResaleByIdUseCase>();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
