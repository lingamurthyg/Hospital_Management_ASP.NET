using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

public record DoctorDto(
    int Id,
    string Name,
    string Email,
    string Phone,
    Gender Gender,
    int DepartmentId,
    string DepartmentName,
    decimal ChargesPerVisit,
    string Specialization,
    string Qualification,
    int WorkExperience,
    float ReputationIndex,
    int PatientsTreated
);

public record DoctorCreateDto(
    string Name,
    string Email,
    string Password,
    string Phone,
    string Address,
    DateTime BirthDate,
    Gender Gender,
    int DepartmentId,
    int WorkExperience,
    decimal Salary,
    decimal ChargesPerVisit,
    string Specialization,
    string Qualification
);

public record DoctorUpdateDto(
    int Id,
    string Name,
    string Phone,
    string Address,
    decimal ChargesPerVisit,
    string Specialization
);
