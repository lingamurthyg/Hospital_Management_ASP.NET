namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Department entity representing medical departments
/// </summary>
public class Department : BaseEntity
{
    public int DeptNo { get; set; }
    public string DeptName { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
