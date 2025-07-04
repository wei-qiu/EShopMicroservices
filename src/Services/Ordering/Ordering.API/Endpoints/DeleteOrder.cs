using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
	/// <summary>
	/// Accepts a order id as a parameter.
	/// Constructs a DeteleOrderCommand based on the order id.
	/// Send the DeleteOrderCommand to the MediatR pipeline.
	/// Returns a success or not found response.
	/// </summary>
	
	//public record DeleteOrderRequest(Guid OrderId);

	public record DeleteOrderResponse(bool IsSuccess);

	public class DeleteOrder : ICarterModule
	{
		//public void AddRoutes(IEndpointRouteBuilder app)
		//{
		//	app.MapDelete("/orders/{id}", async (DeleteOrderRequest request, ISender sender) =>
		//	{
		//		//var command = new DeleteOrderCommand(request.OrderId);
		//		var command = request.Adapt<DeleteOrderCommand>();
		//		var result = await sender.Send(command);
		//		var response = request.Adapt<DeleteOrderResponse>();
		//		return Results.Ok(response);
		//		//return result.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
		//	})
		//	.WithName("DeleteOrder")
		//	.Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
		//	.ProducesProblem(StatusCodes.Status400BadRequest)
		//	.ProducesProblem(StatusCodes.Status404NotFound)
		//	.WithSummary("Update Order")
		//	.WithDescription("Update Order");
		//}

		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
			{
				var result = await sender.Send(new DeleteOrderCommand(Id));

				var response = result.Adapt<DeleteOrderResponse>();

				return Results.Ok(response);
			})
			.WithName("DeleteOrder")
			.Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Delete Order")
			.WithDescription("Delete Order");
		}
	}
}
