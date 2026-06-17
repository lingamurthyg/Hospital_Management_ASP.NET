namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Feedback entity representing patient feedback for an appointment
/// </summary>
public class Feedback
{
    public int FeedbackID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Appointment? Appointment { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
