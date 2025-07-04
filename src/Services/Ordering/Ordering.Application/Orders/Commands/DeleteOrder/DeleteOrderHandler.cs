using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
	{
		public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
		{
			// Logic to delete the Order
			// For example, using a repository to remove the Order from the database
			var orderId = OrderId.Of(command.OrderId);
			var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);
			if(order == null)
			{
				throw new OrderNotFoundException(orderId.Value);
			}

			dbContext.Orders.Remove(order);
			await dbContext.SaveChangesAsync();

			return new DeleteOrderResult(true);
		}
	}
}
