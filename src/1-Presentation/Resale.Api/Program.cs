using Application.UseCases.Resales.Create;
using Application.UseCases.Resales.GetAll;
using Application.UseCases.Resales.GetByFilter;
using Application.UseCases.Resales.GetById;
using Application.UseCases.Resales.Updade;
using Infrastructure;
using Infrastructure.Repoistories.MongoDb;
using Resale.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
