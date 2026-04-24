using Xunit;
using Moq;
using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Tests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<ILogger<AuthenticationService>> _loggerMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _loggerMock = new Mock<ILogger<AuthenticationService>>();
            _authenticationService = new AuthenticationService(
                _patientRepositoryMock.Object,
                _doctorRepositoryMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task ValidateLoginAsync_PatientCredentials_ShouldReturnSuccess()
        {
            // Arrange
            var email = "patient@email.com";
            var password = "password123";
            var patient = new Patient
            {
                Id = 1,
                Email = email,
                Password = password,
                IsActive = true
            };

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(patient);

            // Act
            var result = await _authenticationService.ValidateLoginAsync(email, password);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(1, result.UserId);
            Assert.Equal(1, result.UserType);
        }

        [Fact]
        public async Task ValidateLoginAsync_DoctorCredentials_ShouldReturnSuccess()
        {
            // Arrange
            var email = "doctor@clinic.com";
            var password = "password456";
            var doctor = new Doctor
            {
                Id = 2,
                Email = email,
                Password = password,
                IsActive = true
            };

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient?)null);

            _doctorRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(doctor);

            // Act
            var result = await _authenticationService.ValidateLoginAsync(email, password);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result.UserId);
            Assert.Equal(2, result.UserType);
        }

        [Fact]
        public async Task ValidateLoginAsync_InvalidCredentials_ShouldReturnFailure()
        {
            // Arrange
            var email = "invalid@email.com";
            var password = "wrongpassword";

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient?)null);

            _doctorRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Doctor?)null);

            // Act
            var result = await _authenticationService.ValidateLoginAsync(email, password);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0, result.UserId);
            Assert.Equal(0, result.UserType);
        }

        [Fact]
        public async Task ValidateLoginAsync_InactivePatient_ShouldReturnFailure()
        {
            // Arrange
            var email = "inactive@email.com";
            var password = "password123";
            var patient = new Patient
            {
                Id = 1,
                Email = email,
                Password = password,
                IsActive = false
            };

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(patient);

            // Act
            var result = await _authenticationService.ValidateLoginAsync(email, password);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateLoginAsync_WrongPassword_ShouldReturnFailure()
        {
            // Arrange
            var email = "patient@email.com";
            var correctPassword = "correctpassword";
            var wrongPassword = "wrongpassword";
            var patient = new Patient
            {
                Id = 1,
                Email = email,
                Password = correctPassword,
                IsActive = true
            };

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(patient);

            // Act
            var result = await _authenticationService.ValidateLoginAsync(email, wrongPassword);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task RegisterPatientAsync_ValidData_ShouldReturnSuccess()
        {
            // Arrange
            var name = "John Doe";
            var birthDate = "1990-01-01";
            var email = "john.doe@email.com";
            var password = "password123";
            var phoneNumber = "+1234567890";
            var gender = "Male";
            var address = "123 Main St";

            _patientRepositoryMock
                .Setup(x => x.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _patientRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient p, CancellationToken ct) =>
                {
                    p.Id = 1;
                    return p;
                });

            // Act
            var result = await _authenticationService.RegisterPatientAsync(
                name, birthDate, email, password, phoneNumber, gender, address);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(1, result.PatientId);
            Assert.Equal("Registration successful", result.Message);
        }

        [Fact]
        public async Task RegisterPatientAsync_ExistingEmail_ShouldReturnFailure()
        {
            // Arrange
            var name = "Jane Doe";
            var birthDate = "1985-05-15";
            var email = "existing@email.com";
            var password = "password123";
            var phoneNumber = "+9876543210";
            var gender = "Female";
            var address = "456 Elm St";

            _patientRepositoryMock
                .Setup(x => x.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _authenticationService.RegisterPatientAsync(
                name, birthDate, email, password, phoneNumber, gender, address);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0, result.PatientId);
            Assert.Equal("Email already exists", result.Message);
        }

        [Fact]
        public async Task RegisterPatientAsync_ShouldSetCreatedDate()
        {
            // Arrange
            var name = "Test User";
            var birthDate = "1995-03-20";
            var email = "test@email.com";
            var password = "password123";
            var phoneNumber = "+1111111111";
            var gender = "Other";
            var address = "789 Oak St";
            Patient? capturedPatient = null;

            _patientRepositoryMock
                .Setup(x => x.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _patientRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient p, CancellationToken ct) =>
                {
                    capturedPatient = p;
                    p.Id = 1;
                    return p;
                });

            // Act
            await _authenticationService.RegisterPatientAsync(
                name, birthDate, email, password, phoneNumber, gender, address);

            // Assert
            Assert.NotNull(capturedPatient);
            Assert.True(capturedPatient.IsActive);
            Assert.NotEqual(DateTime.MinValue, capturedPatient.CreatedDate);
        }

        [Fact]
        public async Task ValidateLoginAsync_ShouldCallPatientRepositoryFirst()
        {
            // Arrange
            var email = "test@email.com";
            var password = "password123";

            _patientRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient?)null);

            _doctorRepositoryMock
                .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Doctor?)null);

            // Act
            await _authenticationService.ValidateLoginAsync(email, password);

            // Assert
            _patientRepositoryMock.Verify(
                x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()),
                Times.Once);
            _doctorRepositoryMock.Verify(
                x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
