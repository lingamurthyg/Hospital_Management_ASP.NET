using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Appointment entity representing patient appointments
/// </summary>
public class Appointment : BaseEntity
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int TimeSlotID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public bool FeedbackGiven { get; set; }
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
    public virtual TimeSlot? TimeSlot { get; set; }
    public virtual Bill? Bill { get; set; }
}
