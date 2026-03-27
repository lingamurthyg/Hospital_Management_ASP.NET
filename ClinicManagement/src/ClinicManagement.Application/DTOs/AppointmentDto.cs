using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

public record AppointmentDto(
    int Id,
    int PatientId,
    string PatientName,
    int DoctorId,
    string DoctorName,
    DateTime AppointmentDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    AppointmentStatus Status,
    string? Disease,
    string? Progress,
    string? Prescription
);
