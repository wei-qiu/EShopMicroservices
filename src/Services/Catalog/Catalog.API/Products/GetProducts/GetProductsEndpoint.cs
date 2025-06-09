﻿
namespace Catalog.API.Products.GetProduct
{
	public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
	public record GetProductsResponse(IEnumerable<Product> Products);


	public class GetProductsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products", async ([AsParameters] GetProductRequest request, 
				ISender sender) =>
			{
				var query = request.Adapt<GetProductsQuery>();

				var result = await sender.Send(query);

				var response = result.Adapt<GetProductsResponse>();

				return Results.Ok(response);
			})
		    .WithName("GetProdcts")
			.Produces<GetProductsResponse>(StatusCodes.Status200OK)
		    .ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Products")
			.WithDescription("Get Product");
		}
	}
}
