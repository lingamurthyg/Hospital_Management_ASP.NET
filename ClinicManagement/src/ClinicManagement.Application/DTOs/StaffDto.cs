using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

public record StaffDto(
    int Id,
    string Name,
    string Phone,
    string Address,
    Gender Gender,
    string Designation,
    decimal Salary,
    string Qualification
);
