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
    /// Service for managing doctor operations
    /// </summary>
    public class DoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IDoctorRepository doctorRepository, ILogger<DoctorService> logger)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            try
            {
                var doctor = await _doctorRepository.GetByIdAsync(id);
                if (doctor == null)
                {
                    _logger.LogWarning("Doctor with ID {DoctorId} not found", id);
                    return null;
                }

                return MapToDto(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctor with ID {DoctorId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await _doctorRepository.GetAllAsync();
                return doctors.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all doctors");
                throw;
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsByDepartmentAsync(int deptNo)
        {
            try
            {
                var doctors = await _doctorRepository.GetByDepartmentAsync(deptNo);
                return doctors.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctors for department {DeptNo}", deptNo);
                throw;
            }
        }

        public async Task<DoctorDto> RegisterDoctorAsync(DoctorRegistrationDto registrationDto)
        {
            try
            {
                // Check if email already exists
                if (await _doctorRepository.EmailExistsAsync(registrationDto.Email))
                {
                    throw new InvalidOperationException("Email already exists");
                }

                var doctor = new Doctor
                {
                    Name = registrationDto.Name,
                    Email = registrationDto.Email,
                    Password = HashPassword(registrationDto.Password),
                    BirthDate = registrationDto.BirthDate,
                    DeptNo = registrationDto.DeptNo,
                    Phone = registrationDto.Phone,
                    Gender = registrationDto.Gender,
                    Address = registrationDto.Address,
                    Experience = registrationDto.Experience,
                    Salary = registrationDto.Salary,
                    ChargesPerVisit = registrationDto.ChargesPerVisit,
                    Specialization = registrationDto.Specialization,
                    Qualification = registrationDto.Qualification,
                    Status = true
                };

                var createdDoctor = await _doctorRepository.AddAsync(doctor);
                _logger.LogInformation("Doctor registered successfully with ID {DoctorId}", createdDoctor.DoctorID);

                return MapToDto(createdDoctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering doctor");
                throw;
            }
        }

        public async Task<bool> ValidateLoginAsync(string email, string password)
        {
            try
            {
                var doctor = await _doctorRepository.GetByEmailAsync(email);
                if (doctor == null || !doctor.Status)
                {
                    return false;
                }

                return doctor.Password == HashPassword(password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating login for email {Email}", email);
                throw;
            }
        }

        private DoctorDto MapToDto(Doctor doctor)
        {
            return new DoctorDto
            {
                DoctorID = doctor.DoctorID,
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Gender = doctor.Gender,
                ChargesPerVisit = doctor.ChargesPerVisit,
                ReputationIndex = doctor.ReputationIndex,
                PatientsTreated = doctor.PatientsTreated,
                Qualification = doctor.Qualification,
                Specialization = doctor.Specialization,
                Experience = doctor.Experience,
                Age = doctor.Age,
                DepartmentName = doctor.Department?.DeptName ?? string.Empty
            };
        }

        private string HashPassword(string password)
        {
            // In production, use BCrypt, Argon2, or ASP.NET Core Identity
            return password;
        }
    }
}
