using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(5002, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});

	options.ListenAnyIP(6002, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});

	options.ListenAnyIP(8080, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});
	// If you use HTTPS, configure that port as well
	options.ListenAnyIP(5052, listenOptions =>
	{
		listenOptions.UseHttps();
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});

	options.ListenAnyIP(6062, listenOptions =>
	{
		listenOptions.UseHttps();
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});

	options.ListenAnyIP(8081, listenOptions =>
	{
		listenOptions.UseHttps();
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
	});
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddHealthChecks();
//builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<DiscountContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();
app.MapGrpcService<DiscountService>();
app.MapHealthChecks("/health");

// Enable gRPC reflection in development mode
//if (app.Environment.IsDevelopment())
//{
//	app.MapGrpcReflectionService();
//}


//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();