using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities;

public class Staff : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Qualification { get; set; } = string.Empty;
}
