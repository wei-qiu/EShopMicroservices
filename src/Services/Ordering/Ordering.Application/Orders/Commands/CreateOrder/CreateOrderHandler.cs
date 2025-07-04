using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
	{
		public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
		{
			// Create order entity from command object  
			var order = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(command.Order.CustomerId),
					OrderName.Of(command.Order.OrderName),
					Address.Of(command.Order.ShippingAddress.FirstName, command.Order.ShippingAddress.LastName, command.Order.ShippingAddress.EmailAddress, 
						command.Order.ShippingAddress.AddressLine, command.Order.ShippingAddress.Country, command.Order.ShippingAddress.State, 
						command.Order.ShippingAddress.ZipCode),
					Address.Of(
						command.Order.BillingAddress.FirstName, 
						command.Order.BillingAddress.LastName,
						command.Order.BillingAddress.EmailAddress,
						command.Order.BillingAddress.AddressLine, 
						command.Order.BillingAddress.Country, 
						command.Order.BillingAddress.State,
						command.Order.BillingAddress.ZipCode),
					Payment.Of(
						command.Order.Payment.CardNumber,
						command.Order.Payment.CardName, 
						command.Order.Payment.Cvv,
						command.Order.Payment.Expiration, 
						command.Order.Payment.PaymentMethod)					
				);

			if(command.Order.OrderItems is not null && command.Order.OrderItems.Any())
			{
				foreach (var item in command.Order.OrderItems)
				{
					order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
				}
			}

			// Save to database  
			dbContext.Orders.Add(order);
			await dbContext.SaveChangesAsync(cancellationToken);

			// Return result with order id  
			return new CreateOrderResult(order.Id.Value);
		}
	}	
}
