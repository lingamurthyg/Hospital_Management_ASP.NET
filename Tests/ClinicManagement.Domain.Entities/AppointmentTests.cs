using Xunit;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities.Tests;

public class AppointmentTests
{
    [Fact]
    public void Appointment_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var appointment = new Appointment();

        // Assert
        Assert.NotNull(appointment);
        Assert.Equal(AppointmentStatus.Pending, appointment.Status);
        Assert.False(appointment.IsFeedbackGiven);
    }

    [Fact]
    public void Appointment_Status_DefaultShouldBePending()
    {
        // Arrange & Act
        var appointment = new Appointment();

        // Assert
        Assert.Equal(AppointmentStatus.Pending, appointment.Status);
    }

    [Fact]
    public void Appointment_SetPatientId_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.PatientId = 101;

        // Assert
        Assert.Equal(101, appointment.PatientId);
    }

    [Fact]
    public void Appointment_SetDoctorId_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.DoctorId = 202;

        // Assert
        Assert.Equal(202, appointment.DoctorId);
    }

    [Fact]
    public void Appointment_SetTimeSlotId_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.TimeSlotId = 303;

        // Assert
        Assert.Equal(303, appointment.TimeSlotId);
    }

    [Fact]
    public void Appointment_SetAppointmentDate_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();
        var date = new DateTime(2024, 12, 25);

        // Act
        appointment.AppointmentDate = date;

        // Assert
        Assert.Equal(date, appointment.AppointmentDate);
    }

    [Theory]
    [InlineData(AppointmentStatus.Pending)]
    [InlineData(AppointmentStatus.Approved)]
    [InlineData(AppointmentStatus.Completed)]
    [InlineData(AppointmentStatus.Cancelled)]
    public void Appointment_SetStatus_ShouldUpdateValue(AppointmentStatus status)
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Status = status;

        // Assert
        Assert.Equal(status, appointment.Status);
    }

    [Fact]
    public void Appointment_SetDisease_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Disease = "Flu";

        // Assert
        Assert.Equal("Flu", appointment.Disease);
    }

    [Fact]
    public void Appointment_SetProgress_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Progress = "Improving";

        // Assert
        Assert.Equal("Improving", appointment.Progress);
    }

    [Fact]
    public void Appointment_SetPrescription_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Prescription = "Take medicine twice daily";

        // Assert
        Assert.Equal("Take medicine twice daily", appointment.Prescription);
    }

    [Fact]
    public void Appointment_SetIsFeedbackGiven_ShouldUpdateValue()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.IsFeedbackGiven = true;

        // Assert
        Assert.True(appointment.IsFeedbackGiven);
    }

    [Fact]
    public void Appointment_NullableProperties_ShouldAcceptNull()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Disease = null;
        appointment.Progress = null;
        appointment.Prescription = null;
        appointment.Patient = null;
        appointment.Doctor = null;
        appointment.TimeSlot = null;
        appointment.Bill = null;
        appointment.Feedback = null;

        // Assert
        Assert.Null(appointment.Disease);
        Assert.Null(appointment.Progress);
        Assert.Null(appointment.Prescription);
        Assert.Null(appointment.Patient);
        Assert.Null(appointment.Doctor);
        Assert.Null(appointment.TimeSlot);
        Assert.Null(appointment.Bill);
        Assert.Null(appointment.Feedback);
    }

    [Fact]
    public void Appointment_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var appointment = new Appointment();
        var date = new DateTime(2024, 6, 15, 10, 30, 0);

        // Act
        appointment.PatientId = 1;
        appointment.DoctorId = 2;
        appointment.TimeSlotId = 3;
        appointment.AppointmentDate = date;
        appointment.Status = AppointmentStatus.Approved;
        appointment.Disease = "Migraine";
        appointment.Progress = "Stable";
        appointment.Prescription = "Rest and medication";
        appointment.IsFeedbackGiven = true;

        // Assert
        Assert.Equal(1, appointment.PatientId);
        Assert.Equal(2, appointment.DoctorId);
        Assert.Equal(3, appointment.TimeSlotId);
        Assert.Equal(date, appointment.AppointmentDate);
        Assert.Equal(AppointmentStatus.Approved, appointment.Status);
        Assert.Equal("Migraine", appointment.Disease);
        Assert.Equal("Stable", appointment.Progress);
        Assert.Equal("Rest and medication", appointment.Prescription);
        Assert.True(appointment.IsFeedbackGiven);
    }
}
