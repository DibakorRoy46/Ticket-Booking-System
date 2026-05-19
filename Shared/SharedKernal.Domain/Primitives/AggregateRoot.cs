using SharedKernal.Domain.Events;

namespace SharedKernal.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    // Private list — no external code can directly add/remove events
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(TId id) : base(id) { }

    protected AggregateRoot() { }

    // Read-only snapshot — consumers can read but not mutate
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Called by Infrastructure after dispatching events
    public void ClearDomainEvents() => _domainEvents.Clear();
}