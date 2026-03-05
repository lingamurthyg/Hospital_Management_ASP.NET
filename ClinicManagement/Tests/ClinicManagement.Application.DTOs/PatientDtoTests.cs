using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class PatientDtoTests
{
    [Fact]
    public void PatientDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var patientDto = new PatientDto();

        // Assert
        Assert.Equal(0, patientDto.Id);
        Assert.Equal(0, patientDto.UserId);
        Assert.Null(patientDto.MedicalHistory);
        Assert.Null(patientDto.UserName);
        Assert.Null(patientDto.UserEmail);
    }

    [Fact]
    public void PatientDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var patientDto = new PatientDto
        {
            Id = 1,
            UserId = 10,
            MedicalHistory = "Diabetes",
            UserName = "John Patient",
            UserEmail = "patient@example.com"
        };

        // Assert
        Assert.Equal(1, patientDto.Id);
        Assert.Equal(10, patientDto.UserId);
        Assert.Equal("Diabetes", patientDto.MedicalHistory);
        Assert.Equal("John Patient", patientDto.UserName);
        Assert.Equal("patient@example.com", patientDto.UserEmail);
    }

    [Fact]
    public void PatientCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var patientCreateDto = new PatientCreateDto();

        // Assert
        Assert.Equal(0, patientCreateDto.UserId);
        Assert.Null(patientCreateDto.MedicalHistory);
    }

    [Fact]
    public void PatientCreateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var patientCreateDto = new PatientCreateDto
        {
            UserId = 5,
            MedicalHistory = "Hypertension"
        };

        // Assert
        Assert.Equal(5, patientCreateDto.UserId);
        Assert.Equal("Hypertension", patientCreateDto.MedicalHistory);
    }

    [Fact]
    public void PatientUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var patientUpdateDto = new PatientUpdateDto();

        // Assert
        Assert.Null(patientUpdateDto.MedicalHistory);
    }

    [Fact]
    public void PatientUpdateDto_ShouldSet_MedicalHistory()
    {
        // Arrange & Act
        var patientUpdateDto = new PatientUpdateDto
        {
            MedicalHistory = "Updated medical history"
        };

        // Assert
        Assert.Equal("Updated medical history", patientUpdateDto.MedicalHistory);
    }

    [Fact]
    public void PatientDto_MedicalHistory_ShouldAcceptNull()
    {
        // Arrange & Act
        var patientDto = new PatientDto { MedicalHistory = null };

        // Assert
        Assert.Null(patientDto.MedicalHistory);
    }

    [Fact]
    public void PatientDto_UserName_ShouldAcceptNull()
    {
        // Arrange & Act
        var patientDto = new PatientDto { UserName = null };

        // Assert
        Assert.Null(patientDto.UserName);
    }

    [Fact]
    public void PatientDto_UserEmail_ShouldAcceptNull()
    {
        // Arrange & Act
        var patientDto = new PatientDto { UserEmail = null };

        // Assert
        Assert.Null(patientDto.UserEmail);
    }
}
