namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Appointment entity representing a patient-doctor appointment
/// </summary>
public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; // Pending, Approved, Completed, Cancelled
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public bool IsPaid { get; set; }
    public decimal? BillAmount { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
    public virtual Feedback? Feedback { get; set; }
}
