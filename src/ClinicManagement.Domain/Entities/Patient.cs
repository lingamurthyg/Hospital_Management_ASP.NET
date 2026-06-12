namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a patient in the clinic management system
/// </summary>
public class Patient
{
    public int PatientID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public virtual ICollection<TreatmentHistory> TreatmentHistories { get; set; } = new List<TreatmentHistory>();
}
