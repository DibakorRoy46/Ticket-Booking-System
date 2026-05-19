using MediatR;
using SharedKernal.Application.Abstractions;
using System.Text.Json;

namespace SharedKernal.Application.Behaviors;

// Only applies to commands that carry an IdempotencyKey
public interface IIdempotentCommand
{
    string IdempotencyKey { get; }
}

public sealed class IdempotencyPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IIdempotentCommand
{
    private readonly IIdempotencyService _idempotencyService;

    public IdempotencyPipelineBehavior(IIdempotencyService idempotencyService)
    {
        _idempotencyService = idempotencyService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // 1. Check if we already processed this key
        var cached = await _idempotencyService.GetResponseAsync(
            request.IdempotencyKey, cancellationToken);

        if (cached is not null)
            return JsonSerializer.Deserialize<TResponse>(cached)!;

        // 2. Execute the actual command
        var response = await next();

        // 3. Store response for future duplicate requests (24h TTL)
        await _idempotencyService.StoreResponseAsync(
            request.IdempotencyKey,
            JsonSerializer.Serialize(response),
            TimeSpan.FromHours(24),
            cancellationToken);

        return response;
    }
}
