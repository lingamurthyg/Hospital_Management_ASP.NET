namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a doctor in the clinic management system
/// </summary>
public class Doctor
{
    public int DoctorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public int DeptNo { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public float ReputationIndex { get; set; }
    public int PatientsTreated { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Department? Department { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
