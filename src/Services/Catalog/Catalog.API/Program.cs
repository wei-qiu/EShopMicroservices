
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// before Buikd() call, Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);	

builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("Database"));	

var app = builder.Build();


//After Build() call, Config the HTTP rquest pipeline
app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health", new HealthCheckOptions{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//	exceptionHandlerApp.Run(async context =>
//	{
//		var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//		if (exception == null)
//		{
//			return;
//		}

//		var problemDetails = new ProblemDetails
//		{
//			Title = exception.Message,
//			Status = StatusCodes.Status500InternalServerError,
//			Detail = exception.StackTrace

//		};

//		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//		logger.LogError(exception, exception.Message);

//		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//		context.Response.ContentType = "application/problem+json";

//		await context.Response.WriteAsJsonAsync(problemDetails);

//	});
//});



app.Run();
