

using Catalog.API.Products.GetProductById;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
	public record GetProductByCategoryResult(IEnumerable<Product> Products);

	public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) :
		IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			//logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);

			var product = await session.Query<Product>().Where(prod => prod.Category.Contains(query.Category)).ToListAsync<Product>(cancellationToken);

			if (product == null)
			{
				//throw new ProductNotFoundException();
			}

			return new GetProductByCategoryResult(product);
		}
	}
}
