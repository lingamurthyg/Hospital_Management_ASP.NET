using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Services
{
    /// <summary>
    /// Service for managing patient operations
    /// </summary>
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository patientRepository, ILogger<PatientService> logger)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning("Patient with ID {PatientId} not found", id);
                    return null;
                }

                return MapToDto(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient with ID {PatientId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            try
            {
                var patients = await _patientRepository.GetAllAsync();
                return patients.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all patients");
                throw;
            }
        }

        public async Task<IEnumerable<PatientDto>> SearchPatientsByNameAsync(string name)
        {
            try
            {
                var patients = await _patientRepository.SearchByNameAsync(name);
                return patients.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching patients by name {Name}", name);
                throw;
            }
        }

        public async Task<PatientDto> RegisterPatientAsync(PatientRegistrationDto registrationDto)
        {
            try
            {
                // Check if email already exists
                if (await _patientRepository.EmailExistsAsync(registrationDto.Email))
                {
                    throw new InvalidOperationException("Email already exists");
                }

                var patient = new Patient
                {
                    Name = registrationDto.Name,
                    Phone = registrationDto.Phone,
                    Address = registrationDto.Address,
                    BirthDate = registrationDto.BirthDate,
                    Gender = registrationDto.Gender,
                    Email = registrationDto.Email,
                    Password = HashPassword(registrationDto.Password) // In production, use proper password hashing
                };

                var createdPatient = await _patientRepository.AddAsync(patient);
                _logger.LogInformation("Patient registered successfully with ID {PatientId}", createdPatient.PatientID);

                return MapToDto(createdPatient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering patient");
                throw;
            }
        }

        public async Task<bool> ValidateLoginAsync(string email, string password)
        {
            try
            {
                var patient = await _patientRepository.GetByEmailAsync(email);
                if (patient == null)
                {
                    return false;
                }

                // In production, use proper password verification
                return patient.Password == HashPassword(password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating login for email {Email}", email);
                throw;
            }
        }

        private PatientDto MapToDto(Patient patient)
        {
            return new PatientDto
            {
                PatientID = patient.PatientID,
                Name = patient.Name,
                Phone = patient.Phone,
                Address = patient.Address,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
                Email = patient.Email,
                Age = patient.Age
            };
        }

        private string HashPassword(string password)
        {
            // In production, use BCrypt, Argon2, or ASP.NET Core Identity
            // This is a placeholder
            return password;
        }
    }
}
