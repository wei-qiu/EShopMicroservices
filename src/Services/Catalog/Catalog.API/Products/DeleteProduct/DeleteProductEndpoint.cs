﻿
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.DeleteProduct
{
	//public record DeleteProductRequest(Guid Id);
	public record DeleteProductResponse(bool IsSuccess);

	public class DeleteProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
			{
				//var command = request.Adapt<DeleteProductCommand>();
				var result = await sender.Send(new DeleteProductCommand(id));
				var response = result.Adapt<DeleteProductResponse>();

				return Results.Ok(response);
			})
			.WithName("DeleteProduct")
			.Produces<CreateProductResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Delete Product")
			.WithDescription("Delete Product");

		}
	}
}
