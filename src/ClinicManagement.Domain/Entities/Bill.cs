namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Bill entity representing a billing record for an appointment
/// </summary>
public class Bill
{
    public int BillID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; } = DateTime.UtcNow;
    public bool IsPaid { get; set; } = false;
    public DateTime? PaidDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Appointment? Appointment { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
