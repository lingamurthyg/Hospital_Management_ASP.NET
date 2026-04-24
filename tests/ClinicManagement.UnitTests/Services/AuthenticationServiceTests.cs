using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;

namespace ClinicManagement.UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<IAdminRepository> _adminRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<ILogger<AuthenticationService>> _loggerMock;
    private readonly AuthenticationService _authService;

    public AuthenticationServiceTests()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _adminRepositoryMock = new Mock<IAdminRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _loggerMock = new Mock<ILogger<AuthenticationService>>();

        _authService = new AuthenticationService(
            _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object,
            _adminRepositoryMock.Object,
            _passwordHasherMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task ValidateLoginAsync_WithValidPatient_ReturnsSuccessWithPatientType()
    {
        var email = "patient@test.com";
        var password = "password123";
        var hashedPassword = "hashedPassword";
        var patient = new Patient
        {
            Id = 1,
            Email = email,
            Password = hashedPassword,
            IsActive = true
        };

        _patientRepositoryMock.Setup(x => x.GetByEmailAsync(email, default))
            .ReturnsAsync(patient);
        _passwordHasherMock.Setup(x => x.VerifyPassword(hashedPassword, password))
            .Returns(true);

        var result = await _authService.ValidateLoginAsync(email, password);

        result.IsSuccess.Should().BeTrue();
        result.UserType.Should().Be(UserType.Patient);
        result.UserId.Should().Be(1);
    }

    [Fact]
    public async Task ValidateLoginAsync_WithInvalidPassword_ReturnsFailure()
    {
        var email = "patient@test.com";
        var password = "wrongpassword";
        var hashedPassword = "hashedPassword";
        var patient = new Patient
        {
            Id = 1,
            Email = email,
            Password = hashedPassword,
            IsActive = true
        };

        _patientRepositoryMock.Setup(x => x.GetByEmailAsync(email, default))
            .ReturnsAsync(patient);
        _passwordHasherMock.Setup(x => x.VerifyPassword(hashedPassword, password))
            .Returns(false);

        var result = await _authService.ValidateLoginAsync(email, password);

        result.IsSuccess.Should().BeFalse();
        result.UserType.Should().BeNull();
        result.UserId.Should().BeNull();
    }
}
