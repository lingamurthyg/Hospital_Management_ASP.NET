using Xunit;
using System;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class AppointmentTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAppointmentWithDefaultValues()
        {
            // Arrange & Act
            var appointment = new Appointment();

            // Assert
            Assert.Equal(0, appointment.AppointmentID);
            Assert.Equal(0, appointment.PatientID);
            Assert.Equal(0, appointment.DoctorID);
            Assert.Equal(default(DateTime), appointment.AppointmentDate);
            Assert.Null(appointment.AppointmentTime);
            Assert.Equal("Pending", appointment.Status);
            Assert.Null(appointment.Reason);
            Assert.Null(appointment.Diagnosis);
            Assert.Null(appointment.Prescription);
            Assert.Equal(default(DateTime), appointment.CreatedDate);
            Assert.Null(appointment.ModifiedDate);
            Assert.False(appointment.IsActive);
            Assert.Null(appointment.Patient);
            Assert.Null(appointment.Doctor);
            Assert.Null(appointment.Bill);
        }

        [Fact]
        public void AppointmentID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            int expectedId = 123;

            // Act
            appointment.AppointmentID = expectedId;

            // Assert
            Assert.Equal(expectedId, appointment.AppointmentID);
        }

        [Fact]
        public void PatientID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            int expectedPatientId = 456;

            // Act
            appointment.PatientID = expectedPatientId;

            // Assert
            Assert.Equal(expectedPatientId, appointment.PatientID);
        }

        [Fact]
        public void DoctorID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            int expectedDoctorId = 789;

            // Act
            appointment.DoctorID = expectedDoctorId;

            // Assert
            Assert.Equal(expectedDoctorId, appointment.DoctorID);
        }

        [Fact]
        public void AppointmentDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            DateTime expectedDate = new DateTime(2024, 7, 1);

            // Act
            appointment.AppointmentDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, appointment.AppointmentDate);
        }

        [Fact]
        public void AppointmentTime_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.AppointmentTime = null;

            // Assert
            Assert.Null(appointment.AppointmentTime);
        }

        [Fact]
        public void AppointmentTime_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            TimeSpan expectedTime = new TimeSpan(10, 30, 0);

            // Act
            appointment.AppointmentTime = expectedTime;

            // Assert
            Assert.Equal(expectedTime, appointment.AppointmentTime);
        }

        [Fact]
        public void Status_ShouldDefaultToPending()
        {
            // Arrange & Act
            var appointment = new Appointment();

            // Assert
            Assert.Equal("Pending", appointment.Status);
        }

        [Fact]
        public void Status_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            string expectedStatus = "Confirmed";

            // Act
            appointment.Status = expectedStatus;

            // Assert
            Assert.Equal(expectedStatus, appointment.Status);
        }

        [Fact]
        public void Status_ShouldAcceptDifferentStatusValues()
        {
            // Arrange
            var appointment = new Appointment();
            string[] statuses = { "Pending", "Confirmed", "Completed", "Cancelled" };

            foreach (var status in statuses)
            {
                // Act
                appointment.Status = status;

                // Assert
                Assert.Equal(status, appointment.Status);
            }
        }

        [Fact]
        public void Reason_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Reason = null;

            // Assert
            Assert.Null(appointment.Reason);
        }

        [Fact]
        public void Reason_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            string expectedReason = "Annual checkup";

            // Act
            appointment.Reason = expectedReason;

            // Assert
            Assert.Equal(expectedReason, appointment.Reason);
        }

        [Fact]
        public void Diagnosis_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Diagnosis = null;

            // Assert
            Assert.Null(appointment.Diagnosis);
        }

        [Fact]
        public void Diagnosis_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            string expectedDiagnosis = "Common cold";

            // Act
            appointment.Diagnosis = expectedDiagnosis;

            // Assert
            Assert.Equal(expectedDiagnosis, appointment.Diagnosis);
        }

        [Fact]
        public void Prescription_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Prescription = null;

            // Assert
            Assert.Null(appointment.Prescription);
        }

        [Fact]
        public void Prescription_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            string expectedPrescription = "Amoxicillin 500mg";

            // Act
            appointment.Prescription = expectedPrescription;

            // Assert
            Assert.Equal(expectedPrescription, appointment.Prescription);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            DateTime expectedCreatedDate = new DateTime(2024, 6, 1);

            // Act
            appointment.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, appointment.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.ModifiedDate = null;

            // Assert
            Assert.Null(appointment.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            appointment.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, appointment.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.IsActive = true;

            // Assert
            Assert.True(appointment.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.IsActive = false;

            // Assert
            Assert.False(appointment.IsActive);
        }

        [Fact]
        public void Patient_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Patient = null;

            // Assert
            Assert.Null(appointment.Patient);
        }

        [Fact]
        public void Patient_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            var patient = new Patient { PatientID = 1, Name = "John Doe" };

            // Act
            appointment.Patient = patient;

            // Assert
            Assert.NotNull(appointment.Patient);
            Assert.Equal(patient, appointment.Patient);
        }

        [Fact]
        public void Doctor_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Doctor = null;

            // Assert
            Assert.Null(appointment.Doctor);
        }

        [Fact]
        public void Doctor_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            var doctor = new Doctor { DoctorID = 1, Name = "Dr. Smith" };

            // Act
            appointment.Doctor = doctor;

            // Assert
            Assert.NotNull(appointment.Doctor);
            Assert.Equal(doctor, appointment.Doctor);
        }

        [Fact]
        public void Bill_ShouldAcceptNullValue()
        {
            // Arrange
            var appointment = new Appointment();

            // Act
            appointment.Bill = null;

            // Assert
            Assert.Null(appointment.Bill);
        }

        [Fact]
        public void Bill_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var appointment = new Appointment();
            var bill = new Bill { BillID = 1, Amount = 100.00m };

            // Act
            appointment.Bill = bill;

            // Assert
            Assert.NotNull(appointment.Bill);
            Assert.Equal(bill, appointment.Bill);
        }

        [Fact]
        public void AppointmentProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 8, 15);
            var expectedTime = new TimeSpan(14, 30, 0);
            var expectedCreatedDate = new DateTime(2024, 8, 1);
            var expectedModifiedDate = new DateTime(2024, 8, 10);

            // Act
            var appointment = new Appointment
            {
                AppointmentID = 999,
                PatientID = 111,
                DoctorID = 222,
                AppointmentDate = expectedDate,
                AppointmentTime = expectedTime,
                Status = "Confirmed",
                Reason = "Follow-up",
                Diagnosis = "Improving",
                Prescription = "Continue medication",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(999, appointment.AppointmentID);
            Assert.Equal(111, appointment.PatientID);
            Assert.Equal(222, appointment.DoctorID);
            Assert.Equal(expectedDate, appointment.AppointmentDate);
            Assert.Equal(expectedTime, appointment.AppointmentTime);
            Assert.Equal("Confirmed", appointment.Status);
            Assert.Equal("Follow-up", appointment.Reason);
            Assert.Equal("Improving", appointment.Diagnosis);
            Assert.Equal("Continue medication", appointment.Prescription);
            Assert.Equal(expectedCreatedDate, appointment.CreatedDate);
            Assert.Equal(expectedModifiedDate, appointment.ModifiedDate);
            Assert.True(appointment.IsActive);
        }

        [Fact]
        public void AppointmentTime_ShouldAcceptMorningTime()
        {
            // Arrange
            var appointment = new Appointment();
            TimeSpan morningTime = new TimeSpan(9, 0, 0);

            // Act
            appointment.AppointmentTime = morningTime;

            // Assert
            Assert.Equal(morningTime, appointment.AppointmentTime);
        }

        [Fact]
        public void AppointmentTime_ShouldAcceptAfternoonTime()
        {
            // Arrange
            var appointment = new Appointment();
            TimeSpan afternoonTime = new TimeSpan(15, 30, 0);

            // Act
            appointment.AppointmentTime = afternoonTime;

            // Assert
            Assert.Equal(afternoonTime, appointment.AppointmentTime);
        }
    }
}
