﻿using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices
			(this IServiceCollection services, IConfiguration configuration)
		{
			//ervices.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
			//services.AddAutoMapper(typeof(DependencyInjection).Assembly);
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
				cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
			});

			//Add Feature Management
			services.AddFeatureManagement();

			//Add async communication using rabbitmq as a consumer
			services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
