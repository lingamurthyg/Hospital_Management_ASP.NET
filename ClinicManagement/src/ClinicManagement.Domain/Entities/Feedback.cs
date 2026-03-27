namespace ClinicManagement.Domain.Entities;

public class Feedback : BaseEntity
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;

    public Appointment? Appointment { get; set; }
    public Patient? Patient { get; set; }
}
