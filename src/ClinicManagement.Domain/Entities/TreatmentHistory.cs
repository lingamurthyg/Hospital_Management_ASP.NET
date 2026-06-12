namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents treatment history for a patient
/// </summary>
public class TreatmentHistory
{
    public int TreatmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int AppointmentID { get; set; }
    public string Disease { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string Prescription { get; set; } = string.Empty;
    public DateTime TreatmentDate { get; set; }
    public string Progress { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    // Navigation properties
    public virtual Patient? Patient { get; set; }
}
