namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a medical bill for patient services
/// </summary>
public class Bill
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int AppointmentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}
