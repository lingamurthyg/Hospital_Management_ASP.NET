using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

/// <summary>
/// Data transfer object for Appointment
/// </summary>
public class AppointmentDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int TimeSlotID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; }
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public bool IsPaid { get; set; }
    public bool FeedbackGiven { get; set; }
}

/// <summary>
/// DTO for creating a new appointment
/// </summary>
public class AppointmentCreateDto
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int TimeSlotID { get; set; }
    public DateTime AppointmentDate { get; set; }
}

/// <summary>
/// DTO for updating appointment prescription
/// </summary>
public class AppointmentPrescriptionDto
{
    public int AppointmentID { get; set; }
    public string Disease { get; set; } = string.Empty;
    public string Progress { get; set; } = string.Empty;
    public string Prescription { get; set; } = string.Empty;
}
