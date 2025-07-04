
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders
{
	public class GetOrderQuerytHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
	{
		public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			var orders = await dbContext.Orders
				.Include(o => o.OrderItems)
				.OrderBy(o => o.OrderName.Value)
				.Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
				.Take(request.PaginationRequest.PageSize)
				.ToListAsync(cancellationToken);
			
			var count = await dbContext.Orders.LongCountAsync(cancellationToken);

			var paginatedResult = new PaginatedResult<OrderDto>(
				request.PaginationRequest.PageIndex,
				request.PaginationRequest.PageSize,
				count,
				orders.ProjectToOderDto());

			return new GetOrdersResult(paginatedResult);
		}
	}
}
