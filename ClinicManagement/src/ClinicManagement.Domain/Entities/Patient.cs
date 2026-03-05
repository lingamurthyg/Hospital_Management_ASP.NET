namespace ClinicManagement.Domain.Entities;

public class Patient
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? MedicalHistory { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }

    public User? User { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
