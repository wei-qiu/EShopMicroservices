namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
	public Guid Id = Guid.NewGuid();
	public DateTime occurredOn = DateTime.Now;
	public string EventType = typeof(IntegrationEvent).AssemblyQualifiedName!;
}
