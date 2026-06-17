namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Base entity class with common properties
/// </summary>
public abstract class BaseEntity
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }
}
