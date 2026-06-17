namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Bill entity representing patient bills
/// </summary>
public class Bill : BaseEntity
{
    public int BillID { get; set; }
    public int PatientID { get; set; }
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public bool IsPaid { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
    public virtual Appointment? Appointment { get; set; }
}
