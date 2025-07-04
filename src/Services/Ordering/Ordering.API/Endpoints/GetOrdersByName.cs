using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
	/// <summary>
	/// Accepts a name paremeter
	/// Constructs a GetOrdersByNameQuery
	/// Retrieves and returns matching orders
	/// </summary>
	
	public record GetOrdersByNameRequest(string Name);

	public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

	public class GetOrdersByName : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders/{name}", async (string name, ISender sender) =>
			{
				var query = new GetOrdersByNameQuery(name);
				var result =await sender.Send(query);
				var response = result.Adapt<GetOrdersByNameResponse>();
				return Results.Ok(new GetOrdersByNameResponse(response.Orders));
			})
			.WithName("GetOrdersByName")
			.Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Get Orders By Name")
			.WithDescription("Get Orders By Name");
		}
	}
}
