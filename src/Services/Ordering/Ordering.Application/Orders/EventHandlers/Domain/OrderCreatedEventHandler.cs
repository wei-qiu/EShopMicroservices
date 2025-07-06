using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
	internal class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
		: INotificationHandler<OrderCreatedEvent>
	{
		public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
		{
			// Handle the event (e.g., log it, send a domainEvent, etc.)
			logger.LogInformation("Domain Event Handled: {DommainEvent}", domainEvent.GetType().Name);

			if (await featureManager.IsEnabledAsync("OrderFullfilment"))
			{
				// Create order created integration event
				var orderCreatedIntegrationEVent = domainEvent.Order.ToOrderDto();

				// Publish the event to the message bus
				await publishEndpoint.Publish(orderCreatedIntegrationEVent, cancellationToken);
			}
		

		}
	}

}
