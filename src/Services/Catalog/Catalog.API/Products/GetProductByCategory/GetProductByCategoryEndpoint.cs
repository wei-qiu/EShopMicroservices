﻿
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
	//public record GetProductByCategoryRequest(string category);
	public record GetProductByCategoryResponse(IEnumerable<Product> Products);

	public class GetProductByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("products/category/{category}", async (string category, ISender sender) =>
			{
				var result = await sender.Send(new GetProductByCategoryQuery(category));
				var response = result.Adapt<GetProductByCategoryResponse>();
				return Results.Ok(response);
			})
			.WithName("GetProductByCategory")
			.Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Create Product By Id")
			.WithDescription("Create Product By Id"); ;
		}
	}
}
