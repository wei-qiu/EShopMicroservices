using MediatR;

namespace Ordering.Domain.Abstractions
{
	public interface IDomainEvent : INotification
	{
		Guid EventId => Guid.NewGuid();

		DateTime OccurredOn => DateTime.UtcNow;

		string EventName => GetType().AssemblyQualifiedName;
	}

}
