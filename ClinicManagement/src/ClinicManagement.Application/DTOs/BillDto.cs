namespace ClinicManagement.Application.DTOs;

public record BillDto(
    int Id,
    int AppointmentId,
    int PatientId,
    string PatientName,
    decimal Amount,
    bool IsPaid,
    DateTime BillDate
);
