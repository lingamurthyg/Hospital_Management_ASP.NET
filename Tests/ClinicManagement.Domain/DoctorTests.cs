using Xunit;
using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ClinicManagement.Domain.Tests
{
    public class DoctorTests
    {
        [Fact]
        public void Doctor_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var doctor = new Doctor();

            // Assert
            Assert.NotNull(doctor.Appointments);
            Assert.Empty(doctor.Appointments);
        }

        [Fact]
        public void Doctor_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedId = 1;

            // Act
            doctor.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, doctor.Id);
        }

        [Fact]
        public void Doctor_Name_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedName = "Dr. John Smith";

            // Act
            doctor.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, doctor.Name);
        }

        [Fact]
        public void Doctor_Email_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedEmail = "john.smith@clinic.com";

            // Act
            doctor.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, doctor.Email);
        }

        [Fact]
        public void Doctor_Password_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedPassword = "SecurePassword123";

            // Act
            doctor.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, doctor.Password);
        }

        [Fact]
        public void Doctor_Specialization_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedSpecialization = "Cardiology";

            // Act
            doctor.Specialization = expectedSpecialization;

            // Assert
            Assert.Equal(expectedSpecialization, doctor.Specialization);
        }

        [Fact]
        public void Doctor_PhoneNumber_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedPhoneNumber = "+1234567890";

            // Act
            doctor.PhoneNumber = expectedPhoneNumber;

            // Assert
            Assert.Equal(expectedPhoneNumber, doctor.PhoneNumber);
        }

        [Fact]
        public void Doctor_Address_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedAddress = "123 Medical Plaza";

            // Act
            doctor.Address = expectedAddress;

            // Assert
            Assert.Equal(expectedAddress, doctor.Address);
        }

        [Fact]
        public void Doctor_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedDate = DateTime.UtcNow;

            // Act
            doctor.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, doctor.CreatedDate);
        }

        [Fact]
        public void Doctor_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();
            var expectedDate = DateTime.UtcNow;

            // Act
            doctor.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, doctor.ModifiedDate);
        }

        [Fact]
        public void Doctor_ModifiedDate_CanBeNull()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.ModifiedDate = null;

            // Assert
            Assert.Null(doctor.ModifiedDate);
        }

        [Fact]
        public void Doctor_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.IsActive = true;

            // Assert
            Assert.True(doctor.IsActive);
        }

        [Fact]
        public void Doctor_Appointments_ShouldAddAppointments()
        {
            // Arrange
            var doctor = new Doctor();
            var appointment = new Appointment { Id = 1, DoctorId = doctor.Id };

            // Act
            doctor.Appointments.Add(appointment);

            // Assert
            Assert.Single(doctor.Appointments);
            Assert.Contains(appointment, doctor.Appointments);
        }

        [Fact]
        public void Doctor_AllProperties_ShouldBeSettable()
        {
            // Arrange & Act
            var doctor = new Doctor
            {
                Id = 1,
                Name = "Dr. Jane Doe",
                Email = "jane.doe@clinic.com",
                Password = "Pass123",
                Specialization = "Neurology",
                PhoneNumber = "+9876543210",
                Address = "456 Hospital Rd",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true
            };

            // Assert
            Assert.Equal(1, doctor.Id);
            Assert.Equal("Dr. Jane Doe", doctor.Name);
            Assert.Equal("jane.doe@clinic.com", doctor.Email);
            Assert.Equal("Pass123", doctor.Password);
            Assert.Equal("Neurology", doctor.Specialization);
            Assert.Equal("+9876543210", doctor.PhoneNumber);
            Assert.Equal("456 Hospital Rd", doctor.Address);
            Assert.True(doctor.IsActive);
        }

        [Fact]
        public void Doctor_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var doctor = new Doctor();

            // Assert
            Assert.Equal(string.Empty, doctor.Name);
            Assert.Equal(string.Empty, doctor.Email);
            Assert.Equal(string.Empty, doctor.Password);
            Assert.Equal(string.Empty, doctor.Specialization);
            Assert.Equal(string.Empty, doctor.PhoneNumber);
            Assert.Equal(string.Empty, doctor.Address);
            Assert.False(doctor.IsActive);
        }
    }
}
