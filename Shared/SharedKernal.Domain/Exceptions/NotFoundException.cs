namespace SharedKernal.Domain.Exceptions;


public sealed class NotFoundException : DomainException
{
    public NotFoundException(string entityName, object id)
        : base("NotFound", $"{entityName} with id '{id}' was not found.")
    {
    }
}