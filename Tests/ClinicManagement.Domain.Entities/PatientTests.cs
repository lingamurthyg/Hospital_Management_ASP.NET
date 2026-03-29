using Xunit;
using System;
using System.Collections.Generic;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class PatientTests
    {
        [Fact]
        public void Constructor_ShouldInitializePatientWithDefaultValues()
        {
            // Arrange & Act
            var patient = new Patient();

            // Assert
            Assert.Equal(0, patient.PatientID);
            Assert.Equal(string.Empty, patient.Name);
            Assert.Null(patient.Phone);
            Assert.Null(patient.Address);
            Assert.Equal(default(DateTime), patient.BirthDate);
            Assert.Equal(string.Empty, patient.Gender);
            Assert.Equal(string.Empty, patient.Email);
            Assert.Equal(string.Empty, patient.Password);
            Assert.Equal(default(DateTime), patient.CreatedDate);
            Assert.Null(patient.ModifiedDate);
            Assert.False(patient.IsActive);
            Assert.NotNull(patient.Appointments);
            Assert.Empty(patient.Appointments);
        }

        [Fact]
        public void PatientID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            int expectedId = 123;

            // Act
            patient.PatientID = expectedId;

            // Assert
            Assert.Equal(expectedId, patient.PatientID);
        }

        [Fact]
        public void Name_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedName = "John Doe";

            // Act
            patient.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, patient.Name);
        }

        [Fact]
        public void Phone_ShouldAcceptNullValue()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.Phone = null;

            // Assert
            Assert.Null(patient.Phone);
        }

        [Fact]
        public void Phone_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedPhone = "555-1234";

            // Act
            patient.Phone = expectedPhone;

            // Assert
            Assert.Equal(expectedPhone, patient.Phone);
        }

        [Fact]
        public void Address_ShouldAcceptNullValue()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.Address = null;

            // Assert
            Assert.Null(patient.Address);
        }

        [Fact]
        public void Address_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedAddress = "123 Main St";

            // Act
            patient.Address = expectedAddress;

            // Assert
            Assert.Equal(expectedAddress, patient.Address);
        }

        [Fact]
        public void BirthDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            DateTime expectedBirthDate = new DateTime(1990, 5, 15);

            // Act
            patient.BirthDate = expectedBirthDate;

            // Assert
            Assert.Equal(expectedBirthDate, patient.BirthDate);
        }

        [Fact]
        public void Gender_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedGender = "Male";

            // Act
            patient.Gender = expectedGender;

            // Assert
            Assert.Equal(expectedGender, patient.Gender);
        }

        [Fact]
        public void Email_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedEmail = "john.doe@example.com";

            // Act
            patient.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, patient.Email);
        }

        [Fact]
        public void Password_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            string expectedPassword = "SecurePassword123";

            // Act
            patient.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, patient.Password);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            DateTime expectedCreatedDate = DateTime.Now;

            // Act
            patient.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, patient.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.ModifiedDate = null;

            // Assert
            Assert.Null(patient.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            patient.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, patient.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.IsActive = true;

            // Assert
            Assert.True(patient.IsActive);
        }

        [Fact]
        public void Appointments_ShouldInitializeAsEmptyCollection()
        {
            // Arrange & Act
            var patient = new Patient();

            // Assert
            Assert.NotNull(patient.Appointments);
            Assert.Empty(patient.Appointments);
        }

        [Fact]
        public void Appointments_ShouldAllowAddingItems()
        {
            // Arrange
            var patient = new Patient();
            var appointment = new Appointment { AppointmentID = 1 };

            // Act
            patient.Appointments.Add(appointment);

            // Assert
            Assert.Single(patient.Appointments);
            Assert.Contains(appointment, patient.Appointments);
        }

        [Fact]
        public void PatientProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedBirthDate = new DateTime(1985, 3, 20);
            var expectedCreatedDate = DateTime.Now;

            // Act
            var patient = new Patient
            {
                PatientID = 100,
                Name = "Jane Smith",
                Phone = "555-5678",
                Address = "456 Oak Ave",
                BirthDate = expectedBirthDate,
                Gender = "Female",
                Email = "jane.smith@example.com",
                Password = "Password456",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedCreatedDate.AddDays(1),
                IsActive = true
            };

            // Assert
            Assert.Equal(100, patient.PatientID);
            Assert.Equal("Jane Smith", patient.Name);
            Assert.Equal("555-5678", patient.Phone);
            Assert.Equal("456 Oak Ave", patient.Address);
            Assert.Equal(expectedBirthDate, patient.BirthDate);
            Assert.Equal("Female", patient.Gender);
            Assert.Equal("jane.smith@example.com", patient.Email);
            Assert.Equal("Password456", patient.Password);
            Assert.Equal(expectedCreatedDate, patient.CreatedDate);
            Assert.Equal(expectedCreatedDate.AddDays(1), patient.ModifiedDate);
            Assert.True(patient.IsActive);
        }

        [Fact]
        public void Name_ShouldAcceptEmptyString()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.Name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, patient.Name);
        }

        [Fact]
        public void Email_ShouldAcceptEmptyString()
        {
            // Arrange
            var patient = new Patient();

            // Act
            patient.Email = string.Empty;

            // Assert
            Assert.Equal(string.Empty, patient.Email);
        }
    }
}
