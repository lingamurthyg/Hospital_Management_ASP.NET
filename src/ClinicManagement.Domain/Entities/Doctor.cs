namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Doctor entity representing doctors in the clinic
/// </summary>
public class Doctor : BaseEntity
{
    public int DoctorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int DeptNo { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public float ReputationIndex { get; set; }
    public int PatientsTreated { get; set; }
    public int Status { get; set; } = 1;

    // Navigation properties
    public virtual Department? Department { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
