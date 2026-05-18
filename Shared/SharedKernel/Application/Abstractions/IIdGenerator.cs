namespace SharedKernel.Application.Abstractions;

public interface IIdGenerator
{
    Guid NewId();
    string NewShortId();  // For human-readable codes: booking refs, ticket codes
}
