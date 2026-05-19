namespace SharedKernel.Application.Abstractions;

public interface IDistributedLockHandle : IAsyncDisposable
{
    bool IsAcquired { get; }
}

public interface IDistributedLock
{
    // Returns a handle — if IsAcquired = false, lock was not obtained
    Task<IDistributedLockHandle> AcquireAsync(
        string resource,             
        TimeSpan expiry,             
        TimeSpan? waitTime = null,   
        CancellationToken cancellationToken = default);
}