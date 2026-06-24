using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ClinicManagement.Tests.Services
{
    /// <summary>
    /// Unit tests for PatientService
    /// </summary>
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _mockRepository;
        private readonly Mock<ILogger<PatientService>> _mockLogger;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _mockRepository = new Mock<IPatientRepository>();
            _mockLogger = new Mock<ILogger<PatientService>>();
            _service = new PatientService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsPatient_WhenPatientExists()
        {
            // Arrange
            var patientId = 1;
            var patient = new Patient
            {
                PatientID = patientId,
                Name = "John Doe",
                Email = "john@example.com",
                Phone = "1234567890",
                Address = "123 Main St",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "M"
            };

            _mockRepository.Setup(r => r.GetByIdAsync(patientId))
                .ReturnsAsync(patient);

            // Act
            var result = await _service.GetPatientByIdAsync(patientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patientId, result.PatientID);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsNull_WhenPatientDoesNotExist()
        {
            // Arrange
            var patientId = 999;
            _mockRepository.Setup(r => r.GetByIdAsync(patientId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await _service.GetPatientByIdAsync(patientId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterPatientAsync_ThrowsException_WhenEmailExists()
        {
            // Arrange
            var registrationDto = new PatientRegistrationDto
            {
                Name = "Jane Doe",
                Email = "jane@example.com",
                Password = "password123",
                Phone = "9876543210",
                Address = "456 Oak St",
                BirthDate = new DateTime(1995, 5, 15),
                Gender = "F"
            };

            _mockRepository.Setup(r => r.EmailExistsAsync(registrationDto.Email))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.RegisterPatientAsync(registrationDto));
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsAllPatients()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { PatientID = 1, Name = "Patient 1", Email = "p1@example.com", BirthDate = DateTime.Now.AddYears(-30), Gender = "M", Phone = "123", Address = "Addr1" },
                new Patient { PatientID = 2, Name = "Patient 2", Email = "p2@example.com", BirthDate = DateTime.Now.AddYears(-25), Gender = "F", Phone = "456", Address = "Addr2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(patients);

            // Act
            var result = await _service.GetAllPatientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<PatientDto>)result).Count);
        }
    }
}
