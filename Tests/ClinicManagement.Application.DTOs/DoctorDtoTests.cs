using Xunit;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs.Tests;

public class DoctorDtoTests
{
    [Fact]
    public void DoctorDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new DoctorDto(1, "Dr. Smith", "smith@clinic.com", "1234567890", Gender.Male, 5, "Cardiology", 150m, "Cardiologist", "MD", 10, 4.5f, 200);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Dr. Smith", dto.Name);
        Assert.Equal("smith@clinic.com", dto.Email);
        Assert.Equal("1234567890", dto.Phone);
        Assert.Equal(Gender.Male, dto.Gender);
        Assert.Equal(5, dto.DepartmentId);
        Assert.Equal("Cardiology", dto.DepartmentName);
        Assert.Equal(150m, dto.ChargesPerVisit);
        Assert.Equal("Cardiologist", dto.Specialization);
        Assert.Equal("MD", dto.Qualification);
        Assert.Equal(10, dto.WorkExperience);
        Assert.Equal(4.5f, dto.ReputationIndex);
        Assert.Equal(200, dto.PatientsTreated);
    }

    [Fact]
    public void DoctorCreateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var birthDate = new DateTime(1980, 5, 15);

        // Act
        var dto = new DoctorCreateDto("Dr. Jane", "jane@clinic.com", "password", "9876543210", "123 St", birthDate, Gender.Female, 3, 8, 75000m, 200m, "Neurology", "MD");

        // Assert
        Assert.Equal("Dr. Jane", dto.Name);
        Assert.Equal("jane@clinic.com", dto.Email);
        Assert.Equal("password", dto.Password);
        Assert.Equal("9876543210", dto.Phone);
        Assert.Equal("123 St", dto.Address);
        Assert.Equal(birthDate, dto.BirthDate);
        Assert.Equal(Gender.Female, dto.Gender);
        Assert.Equal(3, dto.DepartmentId);
        Assert.Equal(8, dto.WorkExperience);
        Assert.Equal(75000m, dto.Salary);
        Assert.Equal(200m, dto.ChargesPerVisit);
        Assert.Equal("Neurology", dto.Specialization);
        Assert.Equal("MD", dto.Qualification);
    }

    [Fact]
    public void DoctorUpdateDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new DoctorUpdateDto(1, "Dr. Updated", "5551234567", "456 Ave", 175m, "Orthopedics");

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Dr. Updated", dto.Name);
        Assert.Equal("5551234567", dto.Phone);
        Assert.Equal("456 Ave", dto.Address);
        Assert.Equal(175m, dto.ChargesPerVisit);
        Assert.Equal("Orthopedics", dto.Specialization);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void DoctorDto_WithDifferentGenders_ShouldInitializeCorrectly(Gender gender)
    {
        // Arrange & Act
        var dto = new DoctorDto(1, "Doctor", "doc@clinic.com", "1111111111", gender, 1, "Dept", 100m, "Spec", "Qual", 5, 4.0f, 100);

        // Assert
        Assert.Equal(gender, dto.Gender);
    }

    [Fact]
    public void DoctorDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var dto1 = new DoctorDto(1, "Dr. Smith", "smith@clinic.com", "1234567890", Gender.Male, 5, "Cardiology", 150m, "Cardiologist", "MD", 10, 4.5f, 200);
        var dto2 = new DoctorDto(1, "Dr. Smith", "smith@clinic.com", "1234567890", Gender.Male, 5, "Cardiology", 150m, "Cardiologist", "MD", 10, 4.5f, 200);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void DoctorDto_WithDifferentChargesPerVisit_ShouldNotBeEqual()
    {
        // Arrange
        var dto1 = new DoctorDto(1, "Dr. Smith", "smith@clinic.com", "1234567890", Gender.Male, 5, "Cardiology", 150m, "Cardiologist", "MD", 10, 4.5f, 200);
        var dto2 = new DoctorDto(1, "Dr. Smith", "smith@clinic.com", "1234567890", Gender.Male, 5, "Cardiology", 200m, "Cardiologist", "MD", 10, 4.5f, 200);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }
}
