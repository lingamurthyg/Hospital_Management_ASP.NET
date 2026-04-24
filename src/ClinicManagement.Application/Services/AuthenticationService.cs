using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

/// <summary>
/// Service for handling authentication operations
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        ILogger<AuthenticationService> logger)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<(bool Success, int UserId, int UserType)> ValidateLoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Validating login for email: {Email}", email);

            var patient = await _patientRepository.GetByEmailAsync(email, cancellationToken);
            if (patient != null && patient.Password == password && patient.IsActive)
            {
                _logger.LogInformation("Patient login successful for ID: {PatientId}", patient.Id);
                return (true, patient.Id, 1);
            }

            var doctor = await _doctorRepository.GetByEmailAsync(email, cancellationToken);
            if (doctor != null && doctor.Password == password && doctor.IsActive)
            {
                _logger.LogInformation("Doctor login successful for ID: {DoctorId}", doctor.Id);
                return (true, doctor.Id, 2);
            }

            _logger.LogWarning("Login failed for email: {Email}", email);
            return (false, 0, 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login for email: {Email}", email);
            throw;
        }
    }

    public async Task<(bool Success, int PatientId, string Message)> RegisterPatientAsync(
        string name,
        string birthDate,
        string email,
        string password,
        string phoneNumber,
        string gender,
        string address,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Registering new patient with email: {Email}", email);

            if (await _patientRepository.EmailExistsAsync(email, cancellationToken))
            {
                _logger.LogWarning("Email already exists: {Email}", email);
                return (false, 0, "Email already exists");
            }

            var patient = new Patient
            {
                Name = name,
                BirthDate = DateTime.Parse(birthDate),
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                Gender = gender,
                Address = address,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            var createdPatient = await _patientRepository.AddAsync(patient, cancellationToken);
            _logger.LogInformation("Patient registered successfully with ID: {PatientId}", createdPatient.Id);

            return (true, createdPatient.Id, "Registration successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering patient with email: {Email}", email);
            throw;
        }
    }
}
