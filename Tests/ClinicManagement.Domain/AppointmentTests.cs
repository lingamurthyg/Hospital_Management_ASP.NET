using Xunit;
using ClinicManagement.Domain.Entities;
using System;

namespace ClinicManagement.Domain.Tests
{
    public class AppointmentTests
    {
        [Fact]
        public void Appointment_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedId = 1;

            // Act
            appointment.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, appointment.Id);
        }

        [Fact]
        public void Appointment_PatientId_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedPatientId = 10;

            // Act
            appointment.PatientId = expectedPatientId;

            // Assert
            Assert.Equal(expectedPatientId, appointment.PatientId);
        }

        [Fact]
        public void Appointment_DoctorId_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedDoctorId = 5;

            // Act
            appointment.DoctorId = expectedDoctorId;

            // Assert
            Assert.Equal(expectedDoctorId, appointment.DoctorId);
        }

        [Fact]
        public void Appointment_AppointmentDate_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedDate = DateTime.UtcNow.AddDays(1);

            // Act
            appointment.AppointmentDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, appointment.AppointmentDate);
        }

        [Fact]
        public void Appointment_Status_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedStatus = "Scheduled";

            // Act
            appointment.Status = expectedStatus;

            // Assert
            Assert.Equal(expectedStatus, appointment.Status);
        }

        [Fact]
        public void Appointment_Notes_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedNotes = "Patient requested morning slot";

            // Act
            appointment.Notes = expectedNotes;

            // Assert
            Assert.Equal(expectedNotes, appointment.Notes);
        }

        [Fact]
        public void Appointment_Notes_CanBeNull()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Notes = null;

            // Assert
            Assert.Null(appointment.Notes);
        }

        [Fact]
        public void Appointment_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedDate = DateTime.UtcNow;

            // Act
            appointment.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, appointment.CreatedDate);
        }

        [Fact]
        public void Appointment_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var expectedDate = DateTime.UtcNow;

            // Act
            appointment.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, appointment.ModifiedDate);
        }

        [Fact]
        public void Appointment_ModifiedDate_CanBeNull()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.ModifiedDate = null;

            // Assert
            Assert.Null(appointment.ModifiedDate);
        }

        [Fact]
        public void Appointment_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.IsActive = true;

            // Assert
            Assert.True(appointment.IsActive);
        }

        [Fact]
        public void Appointment_Patient_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var patient = new Patient { Id = 1, Name = "John Doe" };

            // Act
            appointment.Patient = patient;

            // Assert
            Assert.NotNull(appointment.Patient);
            Assert.Equal(patient, appointment.Patient);
        }

        [Fact]
        public void Appointment_Doctor_ShouldSetAndGetValue()
        {
            // Arrange
            var appointment = new Appointment();
            var doctor = new Doctor { Id = 1, Name = "Dr. Smith" };

            // Act
            appointment.Doctor = doctor;

            // Assert
            Assert.NotNull(appointment.Doctor);
            Assert.Equal(doctor, appointment.Doctor);
        }

        [Fact]
        public void Appointment_AllProperties_ShouldBeSettable()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Jane Smith" };
            var doctor = new Doctor { Id = 2, Name = "Dr. Jones" };

            // Act
            var appointment = new Appointment
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 2,
                AppointmentDate = DateTime.UtcNow.AddDays(2),
                Status = "Confirmed",
                Notes = "Follow-up visit",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true,
                Patient = patient,
                Doctor = doctor
            };

            // Assert
            Assert.Equal(1, appointment.Id);
            Assert.Equal(1, appointment.PatientId);
            Assert.Equal(2, appointment.DoctorId);
            Assert.Equal("Confirmed", appointment.Status);
            Assert.Equal("Follow-up visit", appointment.Notes);
            Assert.True(appointment.IsActive);
            Assert.NotNull(appointment.Patient);
            Assert.NotNull(appointment.Doctor);
        }

        [Fact]
        public void Appointment_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var appointment = new Appointment();

            // Assert
            Assert.Equal(string.Empty, appointment.Status);
            Assert.False(appointment.IsActive);
        }

        [Fact]
        public void Appointment_Status_MultipleValues_ShouldBeSettable()
        {
            // Arrange
            var appointment = new Appointment();

            // Act & Assert - Scheduled
            appointment.Status = "Scheduled";
            Assert.Equal("Scheduled", appointment.Status);

            // Act & Assert - Confirmed
            appointment.Status = "Confirmed";
            Assert.Equal("Confirmed", appointment.Status);

            // Act & Assert - Cancelled
            appointment.Status = "Cancelled";
            Assert.Equal("Cancelled", appointment.Status);

            // Act & Assert - Completed
            appointment.Status = "Completed";
            Assert.Equal("Completed", appointment.Status);
        }
    }
}
