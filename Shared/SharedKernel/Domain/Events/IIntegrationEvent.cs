namespace SharedKernel.Domain.Events;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
    string EventType => GetType().Name;
}
