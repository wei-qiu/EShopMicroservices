
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
	/// <summary>
	// Accepts a CreateOrderRequest object
	// Maps the CreateOrderRequest to a CreateOrderCommand
	// Use MediatR to send the command to the correponding handler
	// Returns a response with the created order id
	/// </summary>

	public record CreateOrderRequest(OrderDto Order);
	public record CreateOrderResponse(Guid Id);

	public class CreateOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			//app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
			//{
			//	//var command = new CreateOrderCommand(request.Order);
			//	var command = request.Adapt<CreateOrderCommand>();
			//	var result = await sender.Send(command); //this will trigger CreateOrderCommandHandler
			//          //return Results.Ok(new CreateOrderResponse(result.Id));
			//	var response = result.Adapt<CreateOrderResponse>();

			//	return Results.Created($"/orders/{response.Id}", response);
			//})
			//.WithName("CreateOrder")
			//.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
			//.ProducesProblem(StatusCodes.Status400BadRequest)
			//.WithSummary("Create a new order")
			//.WithDescription("Create Orders.");

			app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
			{
				var command = request.Adapt<CreateOrderCommand>();

				var result = await sender.Send(command);

				var response = result.Adapt<CreateOrderResponse>();

				return Results.Created($"/orders/{response.Id}", response);
			})
			.WithName("CreateOrder")
			.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Create Order")
			.WithDescription("Create Order");
		}
	}
}
