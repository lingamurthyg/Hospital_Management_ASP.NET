using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

/// <summary>
/// Appointment DTO for data transfer
/// </summary>
public class AppointmentDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int TimeSlotID { get; set; }
    public string TimeSlotInfo { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public bool FeedbackGiven { get; set; }
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
}

/// <summary>
/// Appointment create DTO
/// </summary>
public class AppointmentCreateDto
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int TimeSlotID { get; set; }
    public DateTime AppointmentDate { get; set; }
}

/// <summary>
/// Appointment update DTO
/// </summary>
public class AppointmentUpdateDto
{
    public AppointmentStatus Status { get; set; }
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
}
