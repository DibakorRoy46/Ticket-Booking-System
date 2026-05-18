namespace SharedKernel.Domain.Events;

public interface IEventBus
{
    Task PublishAsync<T>(
        T integrationEvent,
        CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}