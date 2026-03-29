namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a time slot for appointments
/// </summary>
public class TimeSlot
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public DateTime SlotDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual Doctor? Doctor { get; set; }
}
