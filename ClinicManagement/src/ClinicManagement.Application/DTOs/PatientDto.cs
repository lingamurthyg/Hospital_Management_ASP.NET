namespace ClinicManagement.Application.DTOs;

public class PatientDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? MedicalHistory { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
}

public class PatientCreateDto
{
    public int UserId { get; set; }
    public string? MedicalHistory { get; set; }
}

public class PatientUpdateDto
{
    public string? MedicalHistory { get; set; }
}
