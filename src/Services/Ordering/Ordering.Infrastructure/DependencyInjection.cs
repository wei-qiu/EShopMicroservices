using Microsoft.EntityFrameworkCore;
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
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.AddInterceptors(new AuditableEntityInterceptor());
				options.UseSqlServer(connectionString);
			});

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			return services;
		}
	}
}
