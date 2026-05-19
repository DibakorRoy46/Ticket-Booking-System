namespace SharedKernal.Application.Abstractions;

public interface IClockProvider
{
    DateTime UtcNow { get; }
    DateOnly UtcToday { get; }
    DateTimeOffset UtcNowOffset { get; }
}
