namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Feedback entity representing patient feedback for appointments
/// </summary>
public class Feedback : BaseEntity
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime FeedbackDate { get; set; }

    // Navigation properties
    public virtual Appointment? Appointment { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
