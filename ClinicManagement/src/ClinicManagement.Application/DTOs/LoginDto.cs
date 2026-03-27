using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

public record LoginDto(
    string Email,
    string Password
);

public record LoginResultDto(
    bool Success,
    string Message,
    int UserId,
    UserType UserType
);
