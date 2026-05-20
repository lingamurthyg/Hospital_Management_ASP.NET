namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents an appointment between a patient and doctor
/// </summary>
public class Appointment
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int TimeSlotID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty; // Pending, Approved, Completed, Cancelled
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public bool FeedbackGiven { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
    public virtual TimeSlot? TimeSlot { get; set; }
    public virtual Bill? Bill { get; set; }
}
