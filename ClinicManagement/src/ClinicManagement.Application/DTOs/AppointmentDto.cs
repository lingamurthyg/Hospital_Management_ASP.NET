namespace ClinicManagement.Application.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Symptoms { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
    public string? PatientName { get; set; }
    public string? DoctorName { get; set; }
}

public class AppointmentCreateDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Symptoms { get; set; }
    public string Status { get; set; } = "Pending";
}

public class AppointmentUpdateDto
{
    public DateTime? AppointmentDate { get; set; }
    public string? Status { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
}
