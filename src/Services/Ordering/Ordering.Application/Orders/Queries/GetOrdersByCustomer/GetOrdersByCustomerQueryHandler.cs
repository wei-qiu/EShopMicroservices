using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Application.Orders.Queries.GetOrderByCustomer;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
	internal class GetOrdersByCustomerQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
	{
		public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
		{
			var orders = await dbContext.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking()
				.Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
				.OrderBy(o => o.OrderName.Value)
				.ToListAsync(cancellationToken);
			var orderDtos = orders.ProjectToOderDto();
			return new GetOrdersByCustomerResult(orderDtos);
		}
	}
}
