using System.Transactions;

namespace SharedKernal.Application.Abstractions;

public interface ITransactionContext : IAsyncDisposable
{
    bool HasActiveTransaction { get; }

    Task BeginAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}