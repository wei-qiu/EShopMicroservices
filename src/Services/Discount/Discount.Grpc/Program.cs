using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
//builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<DiscountContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();

// Enable gRPC reflection in development mode
//if (app.Environment.IsDevelopment())
//{
//	app.MapGrpcReflectionService();
//}


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();