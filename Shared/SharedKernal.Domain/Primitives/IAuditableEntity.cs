namespace SharedKernal.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? ModifiedOnUtc { get; set; }
    string? CreatedBy { get; set; }
    string? ModifiedBy { get; set; }
}
