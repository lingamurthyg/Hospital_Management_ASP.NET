namespace ClinicManagement.Domain.Entities;

public class Bill : BaseEntity
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime BillDate { get; set; } = DateTime.UtcNow;

    public Appointment? Appointment { get; set; }
    public Patient? Patient { get; set; }
}
