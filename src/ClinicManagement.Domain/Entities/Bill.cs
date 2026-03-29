namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a bill for an appointment
/// </summary>
public class Bill
{
    public int BillID { get; set; }
    public int PatientID { get; set; }
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
    public virtual Appointment? Appointment { get; set; }
}
