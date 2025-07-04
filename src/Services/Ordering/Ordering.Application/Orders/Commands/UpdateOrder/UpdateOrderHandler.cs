using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
	internal class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
	{
		public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
		{
			// Find the Order
			//var order = await dbContext.Orders
			//	.Include(o => o.OrderItems)
			//	.FirstOrDefaultAsync(o => o.Id.Value == command.Order.Id, cancellationToken);
			var orderId = OrderId.Of(command.Order.Id);
			var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
			if (order is null)
			{
				// Handle not found (throw, return error result, etc.)
				throw new OrderNotFoundException(command.Order.Id);
				//return new UpdateOrderResult(false);
			}

			// Update Order properties
			order.Update(
				OrderName.Of(command.Order.OrderName),
				Address.Of(
					command.Order.ShippingAddress.FirstName,
					command.Order.ShippingAddress.LastName,
					command.Order.ShippingAddress.EmailAddress,
					command.Order.ShippingAddress.AddressLine,
					command.Order.ShippingAddress.Country,
					command.Order.ShippingAddress.State,
					command.Order.ShippingAddress.ZipCode
				),
				Address.Of(
					command.Order.BillingAddress.FirstName,
					command.Order.BillingAddress.LastName,
					command.Order.BillingAddress.EmailAddress,
					command.Order.BillingAddress.AddressLine,
					command.Order.BillingAddress.Country,
					command.Order.BillingAddress.State,
					command.Order.BillingAddress.ZipCode
				),
				Payment.Of(
					command.Order.Payment.CardNumber,
					command.Order.Payment.CardName,
					command.Order.Payment.Cvv,
					command.Order.Payment.Expiration,
					command.Order.Payment.PaymentMethod
				),
				command.Order.Status
			);

			if (command.Order.OrderItems is not null && command.Order.OrderItems.Any())
			{
				order.OrderItems.ToList().Clear(); // Clear existing items if you want to replace them
				foreach (var item in command.Order.OrderItems)
				{
					order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
				}
			}

			// Optionally update Order items if included in the command
			// (You may want to clear and re-add, or update in place, depending on your requirements)

			dbContext.Orders.Update(order);
			await dbContext.SaveChangesAsync(cancellationToken);

			return new UpdateOrderResult(true);
		}
	}
}
