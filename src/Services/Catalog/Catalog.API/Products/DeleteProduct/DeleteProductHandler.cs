
using Catalog.API.Products.UpdateProduct;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
	public record DeleteProductResult(bool IsSuccess);

	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
		}
	}

	public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) :
		ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
			//logger.LogInformation("DeleteProdctHandler.Handler called with {@Command}", command);

			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product == null)
			{
				throw new ProductNotFoundException(command.Id);
			}

			session.Delete<Product>(product.Id);
			await session.SaveChangesAsync();

			return new DeleteProductResult(true);
		}
	}
}
