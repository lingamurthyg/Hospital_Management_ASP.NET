namespace ClinicManagement.Domain.Entities;

public class Department
{
    public int DeptNo { get; set; }
    public string DeptName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
