namespace SharedKernel.Application.Abstractions;

public interface IClock
{
    DateTime UtcNow { get; }
    DateOnly UtcToday { get; }
    DateTimeOffset UtcNowOffset { get; }
}
