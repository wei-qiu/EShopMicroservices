using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
	/// <summary>
	/// Accespts a pagination parameters
	/// Construct a GetOrderQuery with these parameters
	/// Retrieves a paginated list of orders
	/// </summary>
	
	//public record GetOrderRequest(PaginationRequest Request);

	public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

	public class GetOrders : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			//app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
			//{

			//	//var query = new GetOrdersQuery(request.Request.PageNumber, request.Request.PageSize);
			//	var query = request.Adapt<GetOrderQuery>();
			//	//var query = new GetOrderQuery(request);
			//	var result = await sender.Send(query);
			//	var response = result.Adapt<GetOrderResponse>();
			//	return Results.Ok(response);
			//})
			//.WithName("GetOrders")
			//.Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
			//.ProducesProblem(StatusCodes.Status400BadRequest)
			//.ProducesProblem(StatusCodes.Status404NotFound)
			//.WithSummary("Get Orders")
			//.WithDescription("Get Orders");

			app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersQuery(request));

				var response = result.Adapt<GetOrdersResponse>();

				return Results.Ok(response);
			})
			.WithName("GetOrders")
			.Produces<GetOrdersResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Get Orders")
			.WithDescription("Get Orders");
		}
	}
}
