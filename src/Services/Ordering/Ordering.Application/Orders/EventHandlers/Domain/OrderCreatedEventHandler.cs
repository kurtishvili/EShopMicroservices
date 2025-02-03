namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event successfully handled {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}