using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IPatientRepository patientRepository, IDoctorRepository doctorRepository, ILogger<AuthenticationService> logger)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByEmailAsync(loginDto.Email, cancellationToken);
        if (patient != null && VerifyPassword(loginDto.Password, patient.PasswordHash))
        {
            return new LoginResultDto(true, "Login successful", patient.Id, UserType.Patient);
        }

        var doctor = await _doctorRepository.GetByEmailAsync(loginDto.Email, cancellationToken);
        if (doctor != null && VerifyPassword(loginDto.Password, doctor.PasswordHash))
        {
            return new LoginResultDto(true, "Login successful", doctor.Id, UserType.Doctor);
        }

        return new LoginResultDto(false, "Invalid credentials", 0, UserType.Patient);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
