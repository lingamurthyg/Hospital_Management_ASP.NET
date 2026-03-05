using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class UserDtoTests
{
    [Fact]
    public void UserDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var userDto = new UserDto();

        // Assert
        Assert.Equal(0, userDto.Id);
        Assert.Equal(string.Empty, userDto.Name);
        Assert.Equal(string.Empty, userDto.Email);
        Assert.Equal(string.Empty, userDto.PhoneNo);
        Assert.Null(userDto.Address);
        Assert.Equal(string.Empty, userDto.Gender);
        Assert.Equal(DateTime.MinValue, userDto.BirthDate);
        Assert.Equal(0, userDto.UserType);
    }

    [Fact]
    public void UserDto_ShouldSet_AllProperties()
    {
        // Arrange
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        var userDto = new UserDto
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            PhoneNo = "1234567890",
            Address = "123 Main St",
            Gender = "Male",
            BirthDate = birthDate,
            UserType = 1
        };

        // Assert
        Assert.Equal(1, userDto.Id);
        Assert.Equal("John Doe", userDto.Name);
        Assert.Equal("john@example.com", userDto.Email);
        Assert.Equal("1234567890", userDto.PhoneNo);
        Assert.Equal("123 Main St", userDto.Address);
        Assert.Equal("Male", userDto.Gender);
        Assert.Equal(birthDate, userDto.BirthDate);
        Assert.Equal(1, userDto.UserType);
    }

    [Fact]
    public void UserCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var userCreateDto = new UserCreateDto();

        // Assert
        Assert.Equal(string.Empty, userCreateDto.Name);
        Assert.Equal(string.Empty, userCreateDto.Email);
        Assert.Equal(string.Empty, userCreateDto.Password);
        Assert.Equal(string.Empty, userCreateDto.PhoneNo);
        Assert.Null(userCreateDto.Address);
        Assert.Equal(string.Empty, userCreateDto.Gender);
        Assert.Equal(DateTime.MinValue, userCreateDto.BirthDate);
        Assert.Equal(0, userCreateDto.UserType);
    }

    [Fact]
    public void UserCreateDto_ShouldSet_AllProperties()
    {
        // Arrange
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        var userCreateDto = new UserCreateDto
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            Password = "Password123",
            PhoneNo = "9876543210",
            Address = "456 Oak Ave",
            Gender = "Female",
            BirthDate = birthDate,
            UserType = 2
        };

        // Assert
        Assert.Equal("Jane Doe", userCreateDto.Name);
        Assert.Equal("jane@example.com", userCreateDto.Email);
        Assert.Equal("Password123", userCreateDto.Password);
        Assert.Equal("9876543210", userCreateDto.PhoneNo);
        Assert.Equal("456 Oak Ave", userCreateDto.Address);
        Assert.Equal("Female", userCreateDto.Gender);
        Assert.Equal(birthDate, userCreateDto.BirthDate);
        Assert.Equal(2, userCreateDto.UserType);
    }

    [Fact]
    public void UserUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var userUpdateDto = new UserUpdateDto();

        // Assert
        Assert.Equal(string.Empty, userUpdateDto.Name);
        Assert.Equal(string.Empty, userUpdateDto.PhoneNo);
        Assert.Null(userUpdateDto.Address);
    }

    [Fact]
    public void UserUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var userUpdateDto = new UserUpdateDto
        {
            Name = "Updated Name",
            PhoneNo = "1111111111",
            Address = "789 Pine Rd"
        };

        // Assert
        Assert.Equal("Updated Name", userUpdateDto.Name);
        Assert.Equal("1111111111", userUpdateDto.PhoneNo);
        Assert.Equal("789 Pine Rd", userUpdateDto.Address);
    }

    [Fact]
    public void LoginDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var loginDto = new LoginDto();

        // Assert
        Assert.Equal(string.Empty, loginDto.Email);
        Assert.Equal(string.Empty, loginDto.Password);
    }

    [Fact]
    public void LoginDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var loginDto = new LoginDto
        {
            Email = "user@example.com",
            Password = "SecurePass"
        };

        // Assert
        Assert.Equal("user@example.com", loginDto.Email);
        Assert.Equal("SecurePass", loginDto.Password);
    }

    [Fact]
    public void LoginResultDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var loginResultDto = new LoginResultDto();

        // Assert
        Assert.False(loginResultDto.Success);
        Assert.Equal(string.Empty, loginResultDto.Message);
        Assert.Equal(0, loginResultDto.UserId);
        Assert.Equal(0, loginResultDto.UserType);
        Assert.Equal(string.Empty, loginResultDto.Name);
    }

    [Fact]
    public void LoginResultDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var loginResultDto = new LoginResultDto
        {
            Success = true,
            Message = "Login successful",
            UserId = 5,
            UserType = 1,
            Name = "Test User"
        };

        // Assert
        Assert.True(loginResultDto.Success);
        Assert.Equal("Login successful", loginResultDto.Message);
        Assert.Equal(5, loginResultDto.UserId);
        Assert.Equal(1, loginResultDto.UserType);
        Assert.Equal("Test User", loginResultDto.Name);
    }

    [Fact]
    public void LoginResultDto_Success_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var loginResultDto = new LoginResultDto { Message = "Failed" };

        // Assert
        Assert.False(loginResultDto.Success);
    }
}
