namespace SharedKernal.Domain.Primitives;

// Base record for all strongly typed IDs
public abstract record StronglyTypedId<TValue>(TValue Value)
    where TValue : notnull
{
    public override string ToString() => Value.ToString()!;
}