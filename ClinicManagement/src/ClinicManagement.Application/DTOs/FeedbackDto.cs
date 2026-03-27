namespace ClinicManagement.Application.DTOs;

public record FeedbackDto(
    int Id,
    int AppointmentId,
    int PatientId,
    int DoctorId,
    int Rating,
    string? Comments,
    DateTime FeedbackDate
);
