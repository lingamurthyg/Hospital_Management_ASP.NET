namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a bill for a patient appointment
/// </summary>
public class Bill
{
    public int BillID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime BillDate { get; set; }
    public DateTime? PaidDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Appointment? Appointment { get; set; }
    public virtual Patient? Patient { get; set; }
}
