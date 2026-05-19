
using SharedKernal.Application.Abstractions;

namespace SharedKernal.Application.Guards;

public static class Guard
{
    public static void AgainstNull<T>(
        T? value,
        string paramName,
        string? message = null) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(
                paramName,
                message ?? $"{paramName} cannot be null.");
    }

    public static void AgainstNullOrEmpty(
        string? value,
        string paramName,
        string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                message ?? $"{paramName} cannot be null or empty.",
                paramName);
    }

    public static void AgainstEmptyGuid(
        Guid value,
        string paramName,
        string? message = null)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                message ?? $"{paramName} cannot be an empty GUID.",
                paramName);
    }

    public static void AgainstNegative(
        decimal value,
        string paramName,
        string? message = null)
    {
        if (value < 0)
            throw new ArgumentException(
                message ?? $"{paramName} cannot be negative.",
                paramName);
    }

    public static void AgainstZeroOrNegative(
        decimal value,
        string paramName,
        string? message = null)
    {
        if (value <= 0)
            throw new ArgumentException(
                message ?? $"{paramName} must be greater than zero.",
                paramName);
    }

    public static void AgainstOutOfRange<T>(
        T value,
        T min,
        T max,
        string paramName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"{paramName} must be between {min} and {max}.");
    }

    public static void AgainstInvalidEmail(string email, string paramName)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
            throw new ArgumentException(
                $"{paramName} is not a valid email address.",
                paramName);
    }

    public static void AgainstPastDate(
        DateTime date,
        string paramName,
        IClockProvider? clock = null)
    {
        var now = clock?.UtcNow ?? DateTime.UtcNow;
        if (date < now)
            throw new ArgumentException(
                $"{paramName} cannot be in the past.",
                paramName);
    }

    public static void AgainstExceedingMaxLength(
        string value,
        int maxLength,
        string paramName)
    {
        if (value.Length > maxLength)
            throw new ArgumentException(
                $"{paramName} exceeds maximum length of {maxLength}.",
                paramName);
    }
}
