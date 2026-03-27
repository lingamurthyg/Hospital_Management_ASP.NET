using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public int WorkExperience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public float ReputationIndex { get; set; }
    public int PatientsTreated { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;

    public Department? Department { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
