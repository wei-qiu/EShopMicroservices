using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
	/// <summary>
	// Accepts a UpdateOrderRequest.
	// Maps the UpdateOrderRequest to an UpdateOrderCommand
	// Use MediatR to send the command to the correponding handler
	// Returns a success or error response based on the outcome
	/// </summary>
	/// 

	public record UpdateOrderRequest(OrderDto Order);
	public record UpdateOrderResponse(bool IsSuccess);

	public class UpdateOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/orders", async (UpdateOrderRequest request, ISender mediator) =>
			{
				//var command = new UpdateOrderCommand(request.Order);
				var command = request.Adapt<UpdateOrderCommand>();
				var result = await mediator.Send(command);
				var response = result.Adapt<UpdateOrderResponse>();
				return Results.Ok(response);
				//return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
				//return result.IsSuccess ? Results.Ok(new UpdateOrderResponse(true)) : Results.BadRequest(new UpdateOrderResponse(false));
			})
			.WithName("UpdateOrder")
			.Produces<UpdateOrderResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Update order")
			.WithDescription("Update Order.");
		}
	}
}
