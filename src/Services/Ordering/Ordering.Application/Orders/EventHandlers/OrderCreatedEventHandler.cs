

namespace Ordering.Application.Orders.EventHandlers
{
	internal class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
		: INotificationHandler<OrderCreatedEvent>
	{
		public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{
			// Handle the event (e.g., log it, send a notification, etc.)
			Console.WriteLine($"Order created with ID: {notification.Order.Id}");
			logger.LogInformation("Domain Event Handled: {DommainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}

}
