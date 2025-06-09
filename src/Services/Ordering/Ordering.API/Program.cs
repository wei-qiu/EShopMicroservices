using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices();

var app = builder.Build();

// Configure services using Map or Use methods

// Configure the HTTP request pipeline.
//app.MapGet("/", () => "Hello World!");

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
	await app.InitializeDatabaseAsync();
}


app.Run();
