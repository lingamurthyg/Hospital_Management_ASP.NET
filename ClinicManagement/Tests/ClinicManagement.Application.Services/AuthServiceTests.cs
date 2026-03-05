using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ClinicManagement.Application.Services;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;

namespace ClinicManagement.Tests.Application.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IPatientRepository> _mockPatientRepository;
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly Mock<IStaffRepository> _mockStaffRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<AuthService>> _mockLogger;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockPatientRepository = new Mock<IPatientRepository>();
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _mockStaffRepository = new Mock<IStaffRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<AuthService>>();

        _authService = new AuthService(
            _mockUserRepository.Object,
            _mockPatientRepository.Object,
            _mockDoctorRepository.Object,
            _mockStaffRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password123" };
        var user = new User { Id = 1, Email = "test@example.com", Password = "password123", Name = "Test User", UserType = 1 };

        _mockUserRepository.Setup(r => r.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Login successful", result.Message);
        Assert.Equal(1, result.UserId);
        Assert.Equal(1, result.UserType);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidEmail_ReturnsEmailNotFoundResult()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "invalid@example.com", Password = "password123" };

        _mockUserRepository.Setup(r => r.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ReturnsAsync((User?)null);
        _mockUserRepository.Setup(r => r.EmailExistsAsync(loginDto.Email, default))
            .ReturnsAsync(false);

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Email not found", result.Message);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ReturnsIncorrectPasswordResult()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com", Password = "wrongpassword" };

        _mockUserRepository.Setup(r => r.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ReturnsAsync((User?)null);
        _mockUserRepository.Setup(r => r.EmailExistsAsync(loginDto.Email, default))
            .ReturnsAsync(true);

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Incorrect password", result.Message);
    }

    [Fact]
    public async Task LoginAsync_WhenExceptionThrown_ReturnsErrorResult()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password123" };

        _mockUserRepository.Setup(r => r.ValidateLoginAsync(loginDto.Email, loginDto.Password, default))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("An error occurred during login", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_WithNewEmail_ReturnsSuccessResult()
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            Email = "newuser@example.com",
            Password = "password123",
            Name = "New User",
            UserType = 1,
            PhoneNo = "1234567890",
            Gender = "Male",
            BirthDate = DateTime.Now.AddYears(-30)
        };

        var user = new User
        {
            Id = 1,
            Email = userCreateDto.Email,
            Name = userCreateDto.Name,
            UserType = userCreateDto.UserType
        };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(userCreateDto.Email, default))
            .ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<User>(userCreateDto))
            .Returns(user);
        _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>(), default))
            .ReturnsAsync(user);
        _mockPatientRepository.Setup(r => r.AddAsync(It.IsAny<Patient>(), default))
            .ReturnsAsync(new Patient { Id = 1, UserId = user.Id });

        // Act
        var result = await _authService.RegisterAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Registration successful", result.Message);
        Assert.Equal(1, result.UserId);
        Assert.Equal(1, result.UserType);
        Assert.Equal("New User", result.Name);
    }

    [Fact]
    public async Task RegisterAsync_WithExistingEmail_ReturnsEmailExistsResult()
    {
        // Arrange
        var userCreateDto = new UserCreateDto { Email = "existing@example.com", Password = "password123" };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(userCreateDto.Email, default))
            .ReturnsAsync(true);

        // Act
        var result = await _authService.RegisterAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Email already exists", result.Message);
    }

    [Fact]
    public async Task RegisterAsync_WithUserType1_CreatesPatient()
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            Email = "patient@example.com",
            Password = "password123",
            Name = "Patient User",
            UserType = 1
        };

        var user = new User { Id = 1, Email = userCreateDto.Email, Name = userCreateDto.Name, UserType = 1 };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(userCreateDto.Email, default))
            .ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<User>(userCreateDto))
            .Returns(user);
        _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>(), default))
            .ReturnsAsync(user);
        _mockPatientRepository.Setup(r => r.AddAsync(It.IsAny<Patient>(), default))
            .ReturnsAsync(new Patient { Id = 1, UserId = user.Id });

        // Act
        var result = await _authService.RegisterAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        _mockPatientRepository.Verify(r => r.AddAsync(It.IsAny<Patient>(), default), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_WithUserTypeNot1_DoesNotCreatePatient()
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            Email = "doctor@example.com",
            Password = "password123",
            Name = "Doctor User",
            UserType = 2
        };

        var user = new User { Id = 1, Email = userCreateDto.Email, Name = userCreateDto.Name, UserType = 2 };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(userCreateDto.Email, default))
            .ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<User>(userCreateDto))
            .Returns(user);
        _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>(), default))
            .ReturnsAsync(user);

        // Act
        var result = await _authService.RegisterAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        _mockPatientRepository.Verify(r => r.AddAsync(It.IsAny<Patient>(), default), Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_WhenExceptionThrown_ReturnsErrorResult()
    {
        // Arrange
        var userCreateDto = new UserCreateDto { Email = "test@example.com", Password = "password123" };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(userCreateDto.Email, default))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _authService.RegisterAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("An error occurred during registration", result.Message);
    }
}
