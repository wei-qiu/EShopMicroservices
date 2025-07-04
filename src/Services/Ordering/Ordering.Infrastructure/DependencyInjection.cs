﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.interceptors;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddDbContext<OrderingContext>(options =>
			//	options.UseSqlServer(Configuration.ConnectionString));
			//services.AddScoped<IOrderRepository, OrderRepository>();

			var connectionString = configuration.GetConnectionString("Database");

			services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
			services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

			services.AddDbContext<ApplicationDbContext>((sp, options) =>
			{
				options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
				options.UseSqlServer(connectionString);
			});

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			return services;
		}
	}
}
