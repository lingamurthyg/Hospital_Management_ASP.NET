using Xunit;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs.Tests;

public class LoginDtoTests
{
    [Fact]
    public void LoginDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new LoginDto("user@example.com", "password123");

        // Assert
        Assert.Equal("user@example.com", dto.Email);
        Assert.Equal("password123", dto.Password);
    }

    [Fact]
    public void LoginDto_WithEmptyValues_ShouldAcceptEmptyStrings()
    {
        // Arrange & Act
        var dto = new LoginDto(string.Empty, string.Empty);

        // Assert
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.Password);
    }

    [Fact]
    public void LoginDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var dto1 = new LoginDto("user@example.com", "password123");
        var dto2 = new LoginDto("user@example.com", "password123");

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void LoginResultDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new LoginResultDto(true, "Login successful", 5, UserType.Patient);

        // Assert
        Assert.True(dto.Success);
        Assert.Equal("Login successful", dto.Message);
        Assert.Equal(5, dto.UserId);
        Assert.Equal(UserType.Patient, dto.UserType);
    }

    [Fact]
    public void LoginResultDto_WithFailedLogin_ShouldSetSuccessFalse()
    {
        // Arrange & Act
        var dto = new LoginResultDto(false, "Invalid credentials", 0, UserType.Patient);

        // Assert
        Assert.False(dto.Success);
        Assert.Equal("Invalid credentials", dto.Message);
        Assert.Equal(0, dto.UserId);
    }

    [Theory]
    [InlineData(UserType.Patient)]
    [InlineData(UserType.Doctor)]
    [InlineData(UserType.Admin)]
    [InlineData(UserType.Staff)]
    public void LoginResultDto_WithDifferentUserTypes_ShouldInitializeCorrectly(UserType userType)
    {
        // Arrange & Act
        var dto = new LoginResultDto(true, "Success", 1, userType);

        // Assert
        Assert.Equal(userType, dto.UserType);
    }

    [Fact]
    public void LoginResultDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var dto1 = new LoginResultDto(true, "Login successful", 5, UserType.Patient);
        var dto2 = new LoginResultDto(true, "Login successful", 5, UserType.Patient);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void LoginResultDto_WithDifferentUserIds_ShouldNotBeEqual()
    {
        // Arrange
        var dto1 = new LoginResultDto(true, "Success", 1, UserType.Patient);
        var dto2 = new LoginResultDto(true, "Success", 2, UserType.Patient);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }
}
