using Xunit;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs.Tests;

public class PatientDtoTests
{
    [Fact]
    public void PatientDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var birthDate = new DateTime(1990, 5, 15);

        // Act
        var dto = new PatientDto(1, "John Doe", "john@example.com", "1234567890", "123 Main St", birthDate, Gender.Male, 34, true);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("John Doe", dto.Name);
        Assert.Equal("john@example.com", dto.Email);
        Assert.Equal("1234567890", dto.Phone);
        Assert.Equal("123 Main St", dto.Address);
        Assert.Equal(birthDate, dto.BirthDate);
        Assert.Equal(Gender.Male, dto.Gender);
        Assert.Equal(34, dto.Age);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void PatientCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var birthDate = new DateTime(1985, 3, 20);

        // Act
        var dto = new PatientCreateDto("Jane Smith", "jane@example.com", "password123", "9876543210", "456 Oak Ave", birthDate, Gender.Female);

        // Assert
        Assert.Equal("Jane Smith", dto.Name);
        Assert.Equal("jane@example.com", dto.Email);
        Assert.Equal("password123", dto.Password);
        Assert.Equal("9876543210", dto.Phone);
        Assert.Equal("456 Oak Ave", dto.Address);
        Assert.Equal(birthDate, dto.BirthDate);
        Assert.Equal(Gender.Female, dto.Gender);
    }

    [Fact]
    public void PatientUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new PatientUpdateDto(1, "Updated Name", "5551234567", "789 Pine St");

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Updated Name", dto.Name);
        Assert.Equal("5551234567", dto.Phone);
        Assert.Equal("789 Pine St", dto.Address);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void PatientDto_WithDifferentGenders_ShouldInitializeCorrectly(Gender gender)
    {
        // Arrange
        var birthDate = new DateTime(1995, 7, 10);

        // Act
        var dto = new PatientDto(1, "Test User", "test@example.com", "1111111111", "Test Address", birthDate, gender, 29, true);

        // Assert
        Assert.Equal(gender, dto.Gender);
    }

    [Fact]
    public void PatientDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var birthDate = new DateTime(1990, 5, 15);
        var dto1 = new PatientDto(1, "John Doe", "john@example.com", "1234567890", "123 Main St", birthDate, Gender.Male, 34, true);
        var dto2 = new PatientDto(1, "John Doe", "john@example.com", "1234567890", "123 Main St", birthDate, Gender.Male, 34, true);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void PatientDto_WithInactiveStatus_ShouldSetIsActiveFalse()
    {
        // Arrange
        var birthDate = new DateTime(1980, 1, 1);

        // Act
        var dto = new PatientDto(1, "Inactive User", "inactive@example.com", "0000000000", "No Address", birthDate, Gender.Other, 44, false);

        // Assert
        Assert.False(dto.IsActive);
    }
}
