
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
	public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
	public record CheckoutBasketResult(bool IsSuccess);

	public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
	{
		public CheckoutBasketCommandValidator()
		{
			RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null");
			RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
		}
	}

	public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
	{
		public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
		{
			// get existing basket with total price from db
			var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
			if (basket == null)
			{
				return new CheckoutBasketResult(false);
			}

			// set totalprice on basketcheckout event message
			var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
			eventMessage.TotalPrice = basket.TotalPrice;

			// send basket checkout event to rabbitmq using MassTransit
			await publishEndpoint.Publish(eventMessage, cancellationToken);

			// delete the basket
			await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

			//return CheckoutBasketResult object with success
			return new CheckoutBasketResult(true);
		}
	}
}
