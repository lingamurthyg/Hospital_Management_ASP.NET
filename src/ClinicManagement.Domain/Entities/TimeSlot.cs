namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a time slot for doctor appointments
/// </summary>
public class TimeSlot
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public string Timings { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Doctor? Doctor { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
