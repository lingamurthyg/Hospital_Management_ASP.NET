using Xunit;
using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ClinicManagement.Domain.Tests
{
    public class PatientTests
    {
        [Fact]
        public void Patient_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var patient = new Patient();

            // Assert
            Assert.NotNull(patient.Appointments);
            Assert.Empty(patient.Appointments);
            Assert.NotNull(patient.PatientHistories);
            Assert.Empty(patient.PatientHistories);
        }

        [Fact]
        public void Patient_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedId = 1;

            // Act
            patient.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, patient.Id);
        }

        [Fact]
        public void Patient_Name_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedName = "John Doe";

            // Act
            patient.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, patient.Name);
        }

        [Fact]
        public void Patient_BirthDate_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedBirthDate = new DateTime(1990, 1, 1);

            // Act
            patient.BirthDate = expectedBirthDate;

            // Assert
            Assert.Equal(expectedBirthDate, patient.BirthDate);
        }

        [Fact]
        public void Patient_Email_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedEmail = "john.doe@email.com";

            // Act
            patient.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, patient.Email);
        }

        [Fact]
        public void Patient_Password_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedPassword = "SecurePass123";

            // Act
            patient.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, patient.Password);
        }

        [Fact]
        public void Patient_PhoneNumber_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedPhoneNumber = "+1234567890";

            // Act
            patient.PhoneNumber = expectedPhoneNumber;

            // Assert
            Assert.Equal(expectedPhoneNumber, patient.PhoneNumber);
        }

        [Fact]
        public void Patient_Gender_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedGender = "Male";

            // Act
            patient.Gender = expectedGender;

            // Assert
            Assert.Equal(expectedGender, patient.Gender);
        }

        [Fact]
        public void Patient_Address_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedAddress = "123 Main Street";

            // Act
            patient.Address = expectedAddress;

            // Assert
            Assert.Equal(expectedAddress, patient.Address);
        }

        [Fact]
        public void Patient_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedDate = DateTime.UtcNow;

            // Act
            patient.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, patient.CreatedDate);
        }

        [Fact]
        public void Patient_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();
            var expectedDate = DateTime.UtcNow;

            // Act
            patient.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, patient.ModifiedDate);
        }

        [Fact]
        public void Patient_ModifiedDate_CanBeNull()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.ModifiedDate = null;

            // Assert
            Assert.Null(patient.ModifiedDate);
        }

        [Fact]
        public void Patient_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.IsActive = true;

            // Assert
            Assert.True(patient.IsActive);
        }

        [Fact]
        public void Patient_Appointments_ShouldAddAppointments()
        {
            // Arrange
            var patient = new Patient();
            var appointment = new Appointment { Id = 1, PatientId = patient.Id };

            // Act
            patient.Appointments.Add(appointment);

            // Assert
            Assert.Single(patient.Appointments);
            Assert.Contains(appointment, patient.Appointments);
        }

        [Fact]
        public void Patient_PatientHistories_ShouldAddHistories()
        {
            // Arrange
            var patient = new Patient();
            var history = new PatientHistory { Id = 1, PatientId = patient.Id };

            // Act
            patient.PatientHistories.Add(history);

            // Assert
            Assert.Single(patient.PatientHistories);
            Assert.Contains(history, patient.PatientHistories);
        }

        [Fact]
        public void Patient_AllProperties_ShouldBeSettable()
        {
            // Arrange & Act
            var patient = new Patient
            {
                Id = 1,
                Name = "Jane Smith",
                BirthDate = new DateTime(1985, 5, 15),
                Email = "jane.smith@email.com",
                Password = "Pass123",
                PhoneNumber = "+9876543210",
                Gender = "Female",
                Address = "456 Elm Street",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true
            };

            // Assert
            Assert.Equal(1, patient.Id);
            Assert.Equal("Jane Smith", patient.Name);
            Assert.Equal(new DateTime(1985, 5, 15), patient.BirthDate);
            Assert.Equal("jane.smith@email.com", patient.Email);
            Assert.Equal("Pass123", patient.Password);
            Assert.Equal("+9876543210", patient.PhoneNumber);
            Assert.Equal("Female", patient.Gender);
            Assert.Equal("456 Elm Street", patient.Address);
            Assert.True(patient.IsActive);
        }

        [Fact]
        public void Patient_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var patient = new Patient();

            // Assert
            Assert.Equal(string.Empty, patient.Name);
            Assert.Equal(string.Empty, patient.Email);
            Assert.Equal(string.Empty, patient.Password);
            Assert.Equal(string.Empty, patient.PhoneNumber);
            Assert.Equal(string.Empty, patient.Gender);
            Assert.Equal(string.Empty, patient.Address);
            Assert.False(patient.IsActive);
        }

        [Fact]
        public void Patient_Gender_MultipleValues_ShouldBeSettable()
        {
            // Arrange
            var patient = new Patient();

            // Act & Assert - Male
            patient.Gender = "Male";
            Assert.Equal("Male", patient.Gender);

            // Act & Assert - Female
            patient.Gender = "Female";
            Assert.Equal("Female", patient.Gender);

            // Act & Assert - Other
            patient.Gender = "Other";
            Assert.Equal("Other", patient.Gender);
        }
    }
}
