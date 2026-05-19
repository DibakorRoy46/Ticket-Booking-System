namespace SharedKernal.Application.Abstractions;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
        where T : class;

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    // Cache-Aside pattern
    Task<T> GetOrSetAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
        where T : class;

    // Invalidate all keys matching a pattern 
    Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default);
}