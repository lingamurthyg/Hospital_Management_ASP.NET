namespace ClinicManagement.Application.DTOs;

public record TimeSlotDto(
    int Id,
    int DoctorId,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsAvailable
);
