using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

public record PatientDto(
    int Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    DateTime BirthDate,
    Gender Gender,
    int Age,
    bool IsActive
);

public record PatientCreateDto(
    string Name,
    string Email,
    string Password,
    string Phone,
    string Address,
    DateTime BirthDate,
    Gender Gender
);

public record PatientUpdateDto(
    int Id,
    string Name,
    string Phone,
    string Address
);
