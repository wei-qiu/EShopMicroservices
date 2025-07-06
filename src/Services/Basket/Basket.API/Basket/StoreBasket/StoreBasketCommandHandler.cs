﻿using static Discount.Grpc.Discount;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
	public record StoreBasketResult(string UserName);

	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator()
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
		}
	}

	public class StoreBasketCommandHandler(IBasketRepository repository, DiscountClient discountCliet) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			await DeductDiscount(command.Cart, cancellationToken);

			await repository.StoreBasket(command.Cart, cancellationToken);

			return new StoreBasketResult(command.Cart.UserName);

		}

		private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
		{
			foreach (var item in cart.Items)
			{
				var coupon = await discountCliet.GetDiscountAsync(new() { ProductName = item.ProductName }, cancellationToken: cancellationToken);
				if (coupon != null && coupon.Amount > 0)
				{
					item.Price -= coupon.Amount;
				}
			}
		}
	}
}
