using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
	{
		private readonly IApplicationDbContext _dbContext;

		public CreateOrderHandler(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			// Create order entity from command object  
			var order = Order.Create(
					OrderId.Of(request.Order.Id),
					CustomerId.Of(request.Order.CustomerId),
					OrderName.Of(request.Order.OrderName),
					Address.Of(request.Order.ShippingAddress.FirstName, request.Order.ShippingAddress.LastName, request.Order.ShippingAddress.EmailAddress, 
						request.Order.ShippingAddress.AddressLine, request.Order.ShippingAddress.Country, request.Order.ShippingAddress.State, 
						request.Order.ShippingAddress.ZipCode),
					Address.Of(
						request.Order.BillingAddress.FirstName, 
						request.Order.BillingAddress.LastName,
						request.Order.BillingAddress.EmailAddress,
						request.Order.BillingAddress.AddressLine, 
						request.Order.BillingAddress.Country, 
						request.Order.BillingAddress.State,
						request.Order.BillingAddress.ZipCode),
					Payment.Of(
						request.Order.payment.CardNumber,
						request.Order.payment.CardHolderName, 
						request.Order.payment.Cvv,
						request.Order.payment.Expiration, 
						request.Order.payment.PaymentMethod)					
				);

			if(request.Order.OrderItems is not null && request.Order.OrderItems.Any())
			{
				foreach (var item in request.Order.OrderItems)
				{
					order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
				}
			}




			// Save to database  
			var orders = _dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync(cancellationToken);

			// Return result with order id  
			return new CreateOrderResult(orders.Entity.Id.Value);
		}
	}	
}
