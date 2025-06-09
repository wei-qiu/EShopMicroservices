using Ordering.Domain.Enum;

namespace Ordering.Application.Dtos
{
	public record OrderDto(
		Guid Id,
		Guid CustomerId,
		string OrderName,
		AddressDto ShippingAddress,
		AddressDto BillingAddress,
		PaymentDto payment,
		OrderStatus status,
		List<OrderItemDto> OrderItems
		);

}
