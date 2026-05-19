
namespace SharedKernal.Application.Abstractions;

public interface IIdempotencyService
{
    Task<bool> ExistsAsync(string idempotencyKey, CancellationToken cancellationToken = default);
    Task<string?> GetResponseAsync(string idempotencyKey, CancellationToken cancellationToken = default);
    Task StoreResponseAsync(
        string idempotencyKey,
        string serializedResponse,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);
}
