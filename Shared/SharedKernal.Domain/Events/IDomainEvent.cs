
using MediatR;

namespace SharedKernal.Domain.Events;
public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
