using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class AppointmentDtoTests
{
    [Fact]
    public void AppointmentDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var appointmentDto = new AppointmentDto();

        // Assert
        Assert.Equal(0, appointmentDto.Id);
        Assert.Equal(0, appointmentDto.PatientId);
        Assert.Equal(0, appointmentDto.DoctorId);
        Assert.Equal(DateTime.MinValue, appointmentDto.AppointmentDate);
        Assert.Null(appointmentDto.Symptoms);
        Assert.Equal(string.Empty, appointmentDto.Status);
        Assert.Null(appointmentDto.Diagnosis);
        Assert.Null(appointmentDto.Prescription);
        Assert.Null(appointmentDto.PatientName);
        Assert.Null(appointmentDto.DoctorName);
    }

    [Fact]
    public void AppointmentDto_ShouldSet_AllProperties()
    {
        // Arrange
        var appointmentDate = DateTime.UtcNow;

        // Act
        var appointmentDto = new AppointmentDto
        {
            Id = 1,
            PatientId = 10,
            DoctorId = 5,
            AppointmentDate = appointmentDate,
            Symptoms = "Fever, Cough",
            Status = "Confirmed",
            Diagnosis = "Common Cold",
            Prescription = "Rest and fluids",
            PatientName = "John Doe",
            DoctorName = "Dr. Smith"
        };

        // Assert
        Assert.Equal(1, appointmentDto.Id);
        Assert.Equal(10, appointmentDto.PatientId);
        Assert.Equal(5, appointmentDto.DoctorId);
        Assert.Equal(appointmentDate, appointmentDto.AppointmentDate);
        Assert.Equal("Fever, Cough", appointmentDto.Symptoms);
        Assert.Equal("Confirmed", appointmentDto.Status);
        Assert.Equal("Common Cold", appointmentDto.Diagnosis);
        Assert.Equal("Rest and fluids", appointmentDto.Prescription);
        Assert.Equal("John Doe", appointmentDto.PatientName);
        Assert.Equal("Dr. Smith", appointmentDto.DoctorName);
    }

    [Fact]
    public void AppointmentCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var appointmentCreateDto = new AppointmentCreateDto();

        // Assert
        Assert.Equal(0, appointmentCreateDto.PatientId);
        Assert.Equal(0, appointmentCreateDto.DoctorId);
        Assert.Equal(DateTime.MinValue, appointmentCreateDto.AppointmentDate);
        Assert.Null(appointmentCreateDto.Symptoms);
        Assert.Equal("Pending", appointmentCreateDto.Status);
    }

    [Fact]
    public void AppointmentCreateDto_ShouldSet_AllProperties()
    {
        // Arrange
        var appointmentDate = DateTime.UtcNow;

        // Act
        var appointmentCreateDto = new AppointmentCreateDto
        {
            PatientId = 5,
            DoctorId = 10,
            AppointmentDate = appointmentDate,
            Symptoms = "Headache",
            Status = "Confirmed"
        };

        // Assert
        Assert.Equal(5, appointmentCreateDto.PatientId);
        Assert.Equal(10, appointmentCreateDto.DoctorId);
        Assert.Equal(appointmentDate, appointmentCreateDto.AppointmentDate);
        Assert.Equal("Headache", appointmentCreateDto.Symptoms);
        Assert.Equal("Confirmed", appointmentCreateDto.Status);
    }

    [Fact]
    public void AppointmentUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var appointmentUpdateDto = new AppointmentUpdateDto();

        // Assert
        Assert.Null(appointmentUpdateDto.AppointmentDate);
        Assert.Null(appointmentUpdateDto.Status);
        Assert.Null(appointmentUpdateDto.Diagnosis);
        Assert.Null(appointmentUpdateDto.Prescription);
    }

    [Fact]
    public void AppointmentUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange
        var appointmentDate = DateTime.UtcNow;

        // Act
        var appointmentUpdateDto = new AppointmentUpdateDto
        {
            AppointmentDate = appointmentDate,
            Status = "Completed",
            Diagnosis = "Migraine",
            Prescription = "Pain medication"
        };

        // Assert
        Assert.Equal(appointmentDate, appointmentUpdateDto.AppointmentDate);
        Assert.Equal("Completed", appointmentUpdateDto.Status);
        Assert.Equal("Migraine", appointmentUpdateDto.Diagnosis);
        Assert.Equal("Pain medication", appointmentUpdateDto.Prescription);
    }

    [Fact]
    public void AppointmentDto_Symptoms_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointmentDto = new AppointmentDto { Symptoms = null };

        // Assert
        Assert.Null(appointmentDto.Symptoms);
    }

    [Fact]
    public void AppointmentDto_Diagnosis_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointmentDto = new AppointmentDto { Diagnosis = null };

        // Assert
        Assert.Null(appointmentDto.Diagnosis);
    }

    [Fact]
    public void AppointmentDto_Prescription_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointmentDto = new AppointmentDto { Prescription = null };

        // Assert
        Assert.Null(appointmentDto.Prescription);
    }

    [Fact]
    public void AppointmentCreateDto_Status_DefaultsToPending()
    {
        // Arrange & Act
        var appointmentCreateDto = new AppointmentCreateDto();

        // Assert
        Assert.Equal("Pending", appointmentCreateDto.Status);
    }
}
