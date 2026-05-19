using SharedKernal.Domain.Primitives;

namespace SharedKernal.Application.Abstractions;

public interface IRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId>
    where TId : notnull
{
    Task<TAggregateRoot?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);

    Task AddAsync(TAggregateRoot entity);

    void Update(TAggregateRoot entity);

    void Remove(TAggregateRoot entity);
}