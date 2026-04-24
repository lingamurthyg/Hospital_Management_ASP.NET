namespace ClinicManagement.Application.DTOs;

public class AppointmentDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsPaid { get; set; }
}

public class AppointmentCreateDto
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateTime AppointmentDate { get; set; }
}

public class AppointmentUpdateDto
{
    public string Status { get; set; } = string.Empty;
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsPaid { get; set; }
}
