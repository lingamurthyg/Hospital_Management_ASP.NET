namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Department entity representing a medical department
/// </summary>
public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
