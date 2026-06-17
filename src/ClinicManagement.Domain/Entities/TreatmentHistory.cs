namespace ClinicManagement.Domain.Entities;

/// <summary>
/// TreatmentHistory entity representing patient treatment records
/// </summary>
public class TreatmentHistory : BaseEntity
{
    public int TreatmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateTime TreatmentDate { get; set; }
    public string Disease { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Progress { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
}
