using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
	public static class OrderExtentions
	{
		public static IEnumerable<OrderDto> ProjectToOderDto(this IEnumerable<Order> orders)
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

		public static OrderDto ToOrderDto(this Order order)
		{
			return new OrderDto
			(
				order.Id.Value,
				order.CustomerId.Value,
				order.OrderName.Value,
				new AddressDto
				(
					order.ShippingAddress.FirstName,
					order.ShippingAddress.LastName,
					order.ShippingAddress.EmailAddress,
					order.ShippingAddress.AddressLine,
					order.ShippingAddress.State,
					order.ShippingAddress.ZipCode,
					order.ShippingAddress.Country
				),
				new AddressDto
				(
					order.BillingAddress.FirstName,
					order.BillingAddress.LastName,
					order.BillingAddress.EmailAddress,
					order.BillingAddress.AddressLine,
					order.BillingAddress.State,
					order.BillingAddress.ZipCode,
					order.BillingAddress.Country
				),
				new PaymentDto
				(
					order.Payment.CardName,
					order.Payment.CardNumber,
					order.Payment.CVV,
					order.Payment.Expiration,
					order.Payment.PaymentMethod
				),
				order.Status,
				order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
			);
		}
	}
}
