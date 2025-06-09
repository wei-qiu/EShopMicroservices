﻿
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductRequest(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price);

	public record UpdateProductResponse(bool IsSuccess);

	public class UpdateProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
			{
				var command = request.Adapt<UpdateProductCommand>();
				var result = await sender.Send(command);

				var response = result.Adapt<UpdateProductResponse>();

				return Results.Ok(response);
			})
			.WithName("UpdateProduct")
			.Produces<CreateProductResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Update Product")
			.WithDescription("Update Product"); 
		}
	}
}
