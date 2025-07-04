using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
	public static class OrderExtentions
	{
		public static IEnumerable<OrderDto> ProjectToOderDto(this IEnumerable<Domain.Models.Order> orders)
		{
			return orders.Select(o => new OrderDto
			(
				o.Id.Value,
				o.CustomerId.Value,
				o.OrderName.Value,
				new AddressDto
				(
					o.ShippingAddress.FirstName,
					o.ShippingAddress.LastName,
					o.ShippingAddress.EmailAddress,
					o.ShippingAddress.AddressLine,
					o.ShippingAddress.State,
					o.ShippingAddress.ZipCode,
					o.ShippingAddress.Country
				),
				new AddressDto
				(
					o.BillingAddress.FirstName,
					o.BillingAddress.LastName,
					o.BillingAddress.EmailAddress,
					o.BillingAddress.AddressLine,
					o.BillingAddress.State,
					o.BillingAddress.ZipCode,
					o.BillingAddress.Country
				),
				new PaymentDto
				(
					o.Payment.CardName,
					o.Payment.CardNumber,
					o.Payment.CVV,
					o.Payment.Expiration,
					o.Payment.PaymentMethod
				),
				o.Status,
				o.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
			)).ToList();
		}
	}
}
