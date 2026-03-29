namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a department in the clinic
/// </summary>
public class Department
{
    public int DepartmentID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
