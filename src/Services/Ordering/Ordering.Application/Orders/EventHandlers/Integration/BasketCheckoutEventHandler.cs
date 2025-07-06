using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
	public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
		: IConsumer<BasketCheckoutEvent>
	{
		public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
		{
			//TODO: Create new order and start order fullfillment process
			logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.Message.GetType().Name);
			
			var command = MapToCreateOrderCommand(context.Message);

			await sender.Send(command);
		}

		private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
		{
			//Create order entity from command object
			var addressDto = new AddressDto(
				message.FirstName,
				message.LastName,
				message.EmailAddress,
				message.AddressLine,
				message.State,
				message.ZipCode,
				message.Country);

			var paymentDto = new PaymentDto(
				message.CardNumber,
				message.CardName,
				message.Expiration,
				message.CVV,
				message.PaymentMethod);

			var orderId = Guid.NewGuid();
			var orderDto = new OrderDto(
				orderId,
				message.CustomerId,
				message.UserName,
				addressDto,
				addressDto,
				paymentDto,
				Ordering.Domain.Enum.OrderStatus.Pending,
				new List<OrderItemDto> {
					new OrderItemDto(orderId, new Guid("b1c2d3e4-f5a6-7b8c-9d0e-f1a2b3c4d5e6"), 2, 500),
					new OrderItemDto(orderId, new Guid("f1a2b3c4-d5e6-7b8c-9d0e-b1c2d3e4f5a6"), 1, 400)
				}
			);

			return new CreateOrderCommand(orderDto);
		}
	}
}
