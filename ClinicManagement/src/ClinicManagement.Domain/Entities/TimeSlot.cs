namespace ClinicManagement.Domain.Entities;

public class TimeSlot : BaseEntity
{
    public int DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Doctor? Doctor { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
