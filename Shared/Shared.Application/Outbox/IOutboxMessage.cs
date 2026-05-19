namespace SharedKernal.Application.Outbox;

public interface IOutboxMessage
{
    Guid Id { get; }
    string Type { get; }       // Full type name of the domain event
    string Content { get; }    // Serialized JSON of the domain event
    DateTime CreatedOnUtc { get; }
    DateTime? ProcessedOnUtc { get; }
    string? Error { get; }
    int RetryCount { get; }
}
