using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities;

public class Patient : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
