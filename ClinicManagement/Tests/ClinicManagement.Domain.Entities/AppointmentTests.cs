using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class AppointmentTests
{
    [Fact]
    public void Appointment_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var appointment = new Appointment();

        // Assert
        Assert.Equal(0, appointment.Id);
        Assert.Equal(0, appointment.PatientId);
        Assert.Equal(0, appointment.DoctorId);
        Assert.Equal(DateTime.MinValue, appointment.AppointmentDate);
        Assert.Null(appointment.Symptoms);
        Assert.Equal(string.Empty, appointment.Status);
        Assert.Null(appointment.Diagnosis);
        Assert.Null(appointment.Prescription);
        Assert.False(appointment.IsActive);
        Assert.Equal(string.Empty, appointment.CreatedBy);
        Assert.Null(appointment.ModifiedBy);
        Assert.Null(appointment.Patient);
        Assert.Null(appointment.Doctor);
    }

    [Fact]
    public void Appointment_ShouldSet_IdProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Id = 1;

        // Assert
        Assert.Equal(1, appointment.Id);
    }

    [Fact]
    public void Appointment_ShouldSet_PatientIdProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.PatientId = 5;

        // Assert
        Assert.Equal(5, appointment.PatientId);
    }

    [Fact]
    public void Appointment_ShouldSet_DoctorIdProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.DoctorId = 10;

        // Assert
        Assert.Equal(10, appointment.DoctorId);
    }

    [Fact]
    public void Appointment_ShouldSet_AppointmentDateProperty()
    {
        // Arrange
        var appointment = new Appointment();
        var date = new DateTime(2024, 12, 15, 10, 0, 0);

        // Act
        appointment.AppointmentDate = date;

        // Assert
        Assert.Equal(date, appointment.AppointmentDate);
    }

    [Fact]
    public void Appointment_ShouldSet_SymptomsProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Symptoms = "Fever, Cough";

        // Assert
        Assert.Equal("Fever, Cough", appointment.Symptoms);
    }

    [Fact]
    public void Appointment_ShouldSet_StatusProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Status = "Confirmed";

        // Assert
        Assert.Equal("Confirmed", appointment.Status);
    }

    [Fact]
    public void Appointment_ShouldSet_DiagnosisProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Diagnosis = "Common Cold";

        // Assert
        Assert.Equal("Common Cold", appointment.Diagnosis);
    }

    [Fact]
    public void Appointment_ShouldSet_PrescriptionProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.Prescription = "Rest and fluids";

        // Assert
        Assert.Equal("Rest and fluids", appointment.Prescription);
    }

    [Fact]
    public void Appointment_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var appointment = new Appointment();

        // Act
        appointment.IsActive = true;

        // Assert
        Assert.True(appointment.IsActive);
    }

    [Fact]
    public void Appointment_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var appointment = new Appointment();
        var date = DateTime.UtcNow;

        // Act
        appointment.CreatedDate = date;

        // Assert
        Assert.Equal(date, appointment.CreatedDate);
    }

    [Fact]
    public void Appointment_ShouldSet_PatientProperty()
    {
        // Arrange
        var appointment = new Appointment();
        var patient = new Patient { Id = 1, UserId = 5 };

        // Act
        appointment.Patient = patient;

        // Assert
        Assert.NotNull(appointment.Patient);
        Assert.Equal(1, appointment.Patient.Id);
    }

    [Fact]
    public void Appointment_ShouldSet_DoctorProperty()
    {
        // Arrange
        var appointment = new Appointment();
        var doctor = new Doctor { Id = 1, UserId = 10 };

        // Act
        appointment.Doctor = doctor;

        // Assert
        Assert.NotNull(appointment.Doctor);
        Assert.Equal(1, appointment.Doctor.Id);
    }

    [Fact]
    public void Appointment_Symptoms_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointment = new Appointment { Symptoms = null };

        // Assert
        Assert.Null(appointment.Symptoms);
    }

    [Fact]
    public void Appointment_Diagnosis_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointment = new Appointment { Diagnosis = null };

        // Assert
        Assert.Null(appointment.Diagnosis);
    }

    [Fact]
    public void Appointment_Prescription_ShouldAcceptNull()
    {
        // Arrange & Act
        var appointment = new Appointment { Prescription = null };

        // Assert
        Assert.Null(appointment.Prescription);
    }

    [Fact]
    public void Appointment_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var patient = new Patient { Id = 1 };
        var doctor = new Doctor { Id = 1 };

        // Act
        var appointment = new Appointment
        {
            Id = 15,
            PatientId = 1,
            DoctorId = 1,
            AppointmentDate = date,
            Symptoms = "Headache",
            Status = "Pending",
            Diagnosis = "Migraine",
            Prescription = "Painkillers",
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Doctor",
            Patient = patient,
            Doctor = doctor
        };

        // Assert
        Assert.Equal(15, appointment.Id);
        Assert.Equal(1, appointment.PatientId);
        Assert.Equal(1, appointment.DoctorId);
        Assert.Equal(date, appointment.AppointmentDate);
        Assert.Equal("Headache", appointment.Symptoms);
        Assert.Equal("Pending", appointment.Status);
        Assert.Equal("Migraine", appointment.Diagnosis);
        Assert.Equal("Painkillers", appointment.Prescription);
        Assert.Equal(date, appointment.CreatedDate);
        Assert.Equal(date, appointment.ModifiedDate);
        Assert.True(appointment.IsActive);
        Assert.Equal("System", appointment.CreatedBy);
        Assert.Equal("Doctor", appointment.ModifiedBy);
        Assert.NotNull(appointment.Patient);
        Assert.NotNull(appointment.Doctor);
    }
}
