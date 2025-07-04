using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
	public class OrderNotFoundException : NotFoundException
	{
		public OrderNotFoundException(Guid orderId) : base("Order", orderId)			
		{
		}
		public OrderNotFoundException(string message)
			: base(message)
		{
		}
		public OrderNotFoundException()
			: base("Order not found.")
		{ }
		
	}
}
