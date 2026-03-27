using Xunit;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs.Tests;

public class AppointmentDtoTests
{
    [Fact]
    public void AppointmentDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var appointmentDate = new DateTime(2024, 12, 25, 10, 0, 0);
        var startTime = new TimeSpan(10, 0, 0);
        var endTime = new TimeSpan(11, 0, 0);

        // Act
        var dto = new AppointmentDto(1, 5, "John Doe", 10, "Dr. Smith", appointmentDate, startTime, endTime, AppointmentStatus.Approved, "Flu", "Recovering", "Medicine X");

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(5, dto.PatientId);
        Assert.Equal("John Doe", dto.PatientName);
        Assert.Equal(10, dto.DoctorId);
        Assert.Equal("Dr. Smith", dto.DoctorName);
        Assert.Equal(appointmentDate, dto.AppointmentDate);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.Equal(AppointmentStatus.Approved, dto.Status);
        Assert.Equal("Flu", dto.Disease);
        Assert.Equal("Recovering", dto.Progress);
        Assert.Equal("Medicine X", dto.Prescription);
    }

    [Theory]
    [InlineData(AppointmentStatus.Pending)]
    [InlineData(AppointmentStatus.Approved)]
    [InlineData(AppointmentStatus.Completed)]
    [InlineData(AppointmentStatus.Cancelled)]
    public void AppointmentDto_WithDifferentStatuses_ShouldInitializeCorrectly(AppointmentStatus status)
    {
        // Arrange
        var appointmentDate = new DateTime(2024, 6, 15);
        var startTime = new TimeSpan(14, 0, 0);
        var endTime = new TimeSpan(15, 0, 0);

        // Act
        var dto = new AppointmentDto(1, 5, "Patient", 10, "Doctor", appointmentDate, startTime, endTime, status, null, null, null);

        // Assert
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void AppointmentDto_WithNullOptionalFields_ShouldAcceptNull()
    {
        // Arrange
        var appointmentDate = new DateTime(2024, 3, 10);
        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(10, 0, 0);

        // Act
        var dto = new AppointmentDto(1, 5, "Patient Name", 10, "Doctor Name", appointmentDate, startTime, endTime, AppointmentStatus.Pending, null, null, null);

        // Assert
        Assert.Null(dto.Disease);
        Assert.Null(dto.Progress);
        Assert.Null(dto.Prescription);
    }

    [Fact]
    public void AppointmentDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var appointmentDate = new DateTime(2024, 12, 25);
        var startTime = new TimeSpan(10, 0, 0);
        var endTime = new TimeSpan(11, 0, 0);
        var dto1 = new AppointmentDto(1, 5, "John", 10, "Dr. Smith", appointmentDate, startTime, endTime, AppointmentStatus.Pending, null, null, null);
        var dto2 = new AppointmentDto(1, 5, "John", 10, "Dr. Smith", appointmentDate, startTime, endTime, AppointmentStatus.Pending, null, null, null);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void AppointmentDto_WithDifferentIds_ShouldNotBeEqual()
    {
        // Arrange
        var appointmentDate = new DateTime(2024, 12, 25);
        var startTime = new TimeSpan(10, 0, 0);
        var endTime = new TimeSpan(11, 0, 0);
        var dto1 = new AppointmentDto(1, 5, "John", 10, "Dr. Smith", appointmentDate, startTime, endTime, AppointmentStatus.Pending, null, null, null);
        var dto2 = new AppointmentDto(2, 5, "John", 10, "Dr. Smith", appointmentDate, startTime, endTime, AppointmentStatus.Pending, null, null, null);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }
}
