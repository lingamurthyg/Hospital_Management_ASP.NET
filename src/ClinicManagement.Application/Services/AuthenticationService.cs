using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IAdminRepository adminRepository,
        IPasswordHasher passwordHasher,
        ILogger<AuthenticationService> logger)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, UserType? UserType, int? UserId)> ValidateLoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var patient = await _patientRepository.GetByEmailAsync(email, cancellationToken);
            if (patient != null && patient.IsActive && _passwordHasher.VerifyPassword(patient.Password, password))
            {
                _logger.LogInformation("Patient login successful for email: {Email}", email);
                return (true, UserType.Patient, patient.Id);
            }

            var doctor = await _doctorRepository.GetByEmailAsync(email, cancellationToken);
            if (doctor != null && doctor.IsActive && _passwordHasher.VerifyPassword(doctor.Password, password))
            {
                _logger.LogInformation("Doctor login successful for email: {Email}", email);
                return (true, UserType.Doctor, doctor.Id);
            }

            var admin = await _adminRepository.GetByEmailAsync(email, cancellationToken);
            if (admin != null && admin.IsActive && _passwordHasher.VerifyPassword(admin.Password, password))
            {
                _logger.LogInformation("Admin login successful for email: {Email}", email);
                return (true, UserType.Admin, admin.Id);
            }

            _logger.LogWarning("Login failed for email: {Email}", email);
            return (false, null, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login validation for email: {Email}", email);
            throw;
        }
    }
}
