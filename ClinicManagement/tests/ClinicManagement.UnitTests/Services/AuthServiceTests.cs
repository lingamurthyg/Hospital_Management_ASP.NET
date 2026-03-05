using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicManagement.UnitTests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<IStaffRepository> _staffRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _staffRepositoryMock = new Mock<IStaffRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthService>>();

        _authService = new AuthService(
            _userRepositoryMock.Object,
            _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object,
            _staffRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
    {
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
        var user = new User { Id = 1, Email = "test@example.com", Password = "password", Name = "Test User", UserType = 1 };

        _userRepositoryMock.Setup(x => x.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ReturnsAsync(user);

        var result = await _authService.LoginAsync(loginDto);

        result.Success.Should().BeTrue();
        result.UserId.Should().Be(1);
        result.UserType.Should().Be(1);
    }

    [Fact]
    public async Task LoginAsync_InvalidEmail_ReturnsFailure()
    {
        var loginDto = new LoginDto { Email = "invalid@example.com", Password = "password" };

        _userRepositoryMock.Setup(x => x.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ReturnsAsync((User?)null);
        _userRepositoryMock.Setup(x => x.EmailExistsAsync(loginDto.Email, default))
            .ReturnsAsync(false);

        var result = await _authService.LoginAsync(loginDto);

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("Email not found");
    }
}
