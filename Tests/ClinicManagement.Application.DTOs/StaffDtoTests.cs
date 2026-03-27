using Xunit;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs.Tests;

public class StaffDtoTests
{
    [Fact]
    public void StaffDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new StaffDto(1, "Alice Johnson", "5551234567", "123 Main St", Gender.Female, "Nurse", 45000m, "RN");

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Alice Johnson", dto.Name);
        Assert.Equal("5551234567", dto.Phone);
        Assert.Equal("123 Main St", dto.Address);
        Assert.Equal(Gender.Female, dto.Gender);
        Assert.Equal("Nurse", dto.Designation);
        Assert.Equal(45000m, dto.Salary);
        Assert.Equal("RN", dto.Qualification);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void StaffDto_WithDifferentGenders_ShouldInitializeCorrectly(Gender gender)
    {
        // Arrange & Act
        var dto = new StaffDto(1, "Staff Member", "1111111111", "Address", gender, "Position", 40000m, "Qualification");

        // Assert
        Assert.Equal(gender, dto.Gender);
    }

    [Theory]
    [InlineData(30000.00)]
    [InlineData(45000.50)]
    [InlineData(60000.99)]
    public void StaffDto_WithDifferentSalaries_ShouldInitializeCorrectly(decimal salary)
    {
        // Arrange & Act
        var dto = new StaffDto(1, "Staff", "1234567890", "Address", Gender.Male, "Position", salary, "Qual");

        // Assert
        Assert.Equal(salary, dto.Salary);
    }

    [Fact]
    public void StaffDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var dto1 = new StaffDto(1, "Alice Johnson", "5551234567", "123 Main St", Gender.Female, "Nurse", 45000m, "RN");
        var dto2 = new StaffDto(1, "Alice Johnson", "5551234567", "123 Main St", Gender.Female, "Nurse", 45000m, "RN");

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void StaffDto_WithDifferentDesignations_ShouldNotBeEqual()
    {
        // Arrange
        var dto1 = new StaffDto(1, "Alice Johnson", "5551234567", "123 Main St", Gender.Female, "Nurse", 45000m, "RN");
        var dto2 = new StaffDto(1, "Alice Johnson", "5551234567", "123 Main St", Gender.Female, "Receptionist", 45000m, "RN");

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void StaffDto_WithEmptyValues_ShouldAcceptEmptyStrings()
    {
        // Arrange & Act
        var dto = new StaffDto(0, string.Empty, string.Empty, string.Empty, Gender.Other, string.Empty, 0m, string.Empty);

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(string.Empty, dto.Phone);
        Assert.Equal(string.Empty, dto.Address);
        Assert.Equal(string.Empty, dto.Designation);
        Assert.Equal(string.Empty, dto.Qualification);
        Assert.Equal(0m, dto.Salary);
    }
}
