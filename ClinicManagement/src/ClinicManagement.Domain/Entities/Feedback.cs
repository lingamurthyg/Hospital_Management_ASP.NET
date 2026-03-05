namespace ClinicManagement.Domain.Entities;

public class Feedback
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public DateTime FeedbackDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public Patient? Patient { get; set; }
}
