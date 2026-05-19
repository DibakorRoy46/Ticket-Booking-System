namespace SharedKernal.Domain.Primitives;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    DateTime? DeletedOnUtc { get; }
    string? DeletedBy { get; }
    void SoftDelete(string deletedBy);
}
