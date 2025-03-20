using Autofac;
using Autofac.Extensions.DependencyInjection;
using MongoDB.Driver;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess;
using Business.DependencyResolvers.Autofac;
using Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule()); //
});

builder.Services.AddCors();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("MongoDB"));
var database = mongoClient.GetDatabase("KaleLojistikDB");
builder.Services.AddSingleton(database);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
