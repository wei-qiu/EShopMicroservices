namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile, decimal Price);

	public record CreateProductResponse(Guid Id);

	public class CreateProductEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
			{
				//get command object from request using 
				var command = request.Adapt<CreateProductCommand>();

				//send to CommandHandler
				var result = await sender.Send(command);

				//convert from CreateProductResult to CreateProductReponse object
				var response = result.Adapt<CreateProductResponse>();

				return Results.Created("/products/{response.Id}", response);

			})
			.WithName("CreateProduct")
			.Produces<CreateProductResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Create Product")
			.WithDescription("Create Product");
			
		}
	}
}
