using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.CreateVersion7();
    
    public DateTime OccurredOn => DateTime.UtcNow;

    public string EventType => GetType().AssemblyQualifiedName!;
}