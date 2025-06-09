﻿using Catalog.API.Products.CreateProduct;
using Marten.Linq.SoftDeletes;

namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
	public record UpdateProductResult(bool IsSuccess);

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");

			RuleFor(command => command.Name).NotNull().WithMessage("Product Id is required")
				.Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

			RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}

	public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			//logger.LogInformation("UpdateProdctHandler.Handler called with {@Command}", command);
			//Search the target product
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product == null)
			{
				throw new ProductNotFoundException(command.Id);
			}

			product.Name = command.Name;
			product.Description = command.Description;
			product.Category = command.Category;
			product.ImageFile = command.ImageFile;
			product.Price = command.Price;

			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateProductResult(true);
		}
	}
}
