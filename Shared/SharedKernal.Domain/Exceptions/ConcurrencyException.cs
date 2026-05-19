namespace SharedKernal.Domain.Exceptions;

public sealed class ConcurrencyException : DomainException
{
    public ConcurrencyException(string entityName)
        : base("ConcurrencyConflict", $"A concurrency conflict occurred while saving {entityName}. Please retry.")
    {
    }
}