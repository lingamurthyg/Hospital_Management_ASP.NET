namespace ClinicManagement.Domain.Entities;

/// <summary>
/// TimeSlot entity representing available time slots for appointments
/// </summary>
public class TimeSlot
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public DateTime SlotDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Doctor? Doctor { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
