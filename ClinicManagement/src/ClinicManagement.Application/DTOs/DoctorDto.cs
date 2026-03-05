namespace ClinicManagement.Application.DTOs;

public class DoctorDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string? Qualification { get; set; }
    public decimal? ConsultationFee { get; set; }
    public string? AvailableTimings { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
}

public class DoctorCreateDto
{
    public int UserId { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string? Qualification { get; set; }
    public decimal? ConsultationFee { get; set; }
    public string? AvailableTimings { get; set; }
}

public class DoctorUpdateDto
{
    public string Specialization { get; set; } = string.Empty;
    public string? Qualification { get; set; }
    public decimal? ConsultationFee { get; set; }
    public string? AvailableTimings { get; set; }
}
