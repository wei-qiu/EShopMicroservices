namespace Ordering.Application.Orders.EventHandlers
{
	internal class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
	{
		public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
		{
			logger.LogInformation("Order updated: {OrderId}", notification.Order.Id);
			logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
