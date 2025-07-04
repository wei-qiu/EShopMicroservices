using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints
{
	/// <summary>
	/// Accepts a customer ID.
	/// Construct a GetOrdersByCustomerQuery with the customer ID.
	/// Returns the list of order for the customer.
	/// </summary>
	
	//public record GetOrdersByCustomerRequest(Guid CustomerId);

	public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

	public class GetOrdersByCustomer : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
			{
				var query = new GetOrdersByCustomerQuery(customerId);
				var result = await sender.Send(query);
				var response = result.Adapt<GetOrdersByCustomerResponse>();
				return Results.Ok(response.Orders);
			})
			.WithName("GetOrdersByCustomer")
			.Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Get Orders By Customer")
			.WithDescription("Get Orders By Customer");
		}
	}
}
