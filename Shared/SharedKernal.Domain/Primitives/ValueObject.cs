namespace SharedKernal.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;
        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
        => obj is ValueObject vo && Equals(vo);

    public override int GetHashCode()
        => GetEqualityComponents()
            .Aggregate(default(HashCode), (hashCode, obj) =>
            {
                hashCode.Add(obj);
                return hashCode;
            })
            .ToHashCode();

    public static bool operator ==(ValueObject? left, ValueObject? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !(left == right);

    protected virtual ValueObject GetCopy() =>
        (ValueObject)MemberwiseClone();
}