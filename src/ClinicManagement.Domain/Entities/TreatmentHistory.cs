namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents a treatment history record
/// </summary>
public class TreatmentHistory
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime TreatmentDate { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public virtual Patient Patient { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
}
