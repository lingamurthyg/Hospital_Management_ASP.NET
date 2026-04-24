namespace ClinicManagement.Domain.Entities;

public class Appointment
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsPaid { get; set; }
    public bool? FeedbackGiven { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
