namespace Basket.API.Basket.CheckoutBasket
{
	public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
	public record CheckoutBasketResponse(bool IsSuccess);
	//Expose CheckoutBasket API
	public class CheckoutBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
			{
				// Map the request to a command
				//var command = request.BasketCheckoutDto.Adapt<CheckoutBasketCommand>();
				var command = new CheckoutBasketCommand(request.BasketCheckoutDto);
				// Send the command using MediatR
				var result = await sender.Send(command);

				// Map the result to a response
				var response = result.Adapt<CheckoutBasketResponse>();

				// Return the response
				return Results.Ok(response);
			})
			.WithName("CheckoutBasket")
			.Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Checkout Basket")
			.WithDescription("Checkout Basket");
		}
	}
}
