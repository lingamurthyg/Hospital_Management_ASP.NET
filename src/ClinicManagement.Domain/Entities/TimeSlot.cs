namespace ClinicManagement.Domain.Entities;

/// <summary>
/// TimeSlot entity representing doctor availability slots
/// </summary>
public class TimeSlot : BaseEntity
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime SlotDate { get; set; }

    // Navigation properties
    public virtual Doctor? Doctor { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
