using System.Reflection;

namespace SharedKernal.Domain.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    public int Value { get; protected init; }
    public string Name { get; protected init; }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static IEnumerable<TEnum> GetAll()
        => typeof(TEnum)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(TEnum))
            .Select(f => (TEnum)f.GetValue(null)!)
            .OrderBy(e => e.Value);

    public static TEnum? FromValue(int value)
        => GetAll().SingleOrDefault(e => e.Value == value);

    public static TEnum? FromName(string name)
        => GetAll().SingleOrDefault(e =>
            string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));

    public static TEnum FromValueOrThrow(int value)
        => FromValue(value)
           ?? throw new InvalidOperationException(
               $"No {typeof(TEnum).Name} with value {value} exists.");

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;
        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj)
        => obj is Enumeration<TEnum> e && Equals(e);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;

    public static bool operator ==(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
        => !(left == right);
}