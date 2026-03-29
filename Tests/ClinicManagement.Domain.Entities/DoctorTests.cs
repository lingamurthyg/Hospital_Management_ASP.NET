using Xunit;
using System;
using System.Collections.Generic;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class DoctorTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDoctorWithDefaultValues()
        {
            // Arrange & Act
            var doctor = new Doctor();

            // Assert
            Assert.Equal(0, doctor.DoctorID);
            Assert.Equal(string.Empty, doctor.Name);
            Assert.Equal(string.Empty, doctor.Email);
            Assert.Equal(string.Empty, doctor.Password);
            Assert.Equal(default(DateTime), doctor.BirthDate);
            Assert.Null(doctor.DepartmentID);
            Assert.Null(doctor.Phone);
            Assert.Null(doctor.Address);
            Assert.Equal(string.Empty, doctor.Specialization);
            Assert.Equal(default(DateTime), doctor.CreatedDate);
            Assert.Null(doctor.ModifiedDate);
            Assert.False(doctor.IsActive);
            Assert.Null(doctor.Department);
            Assert.NotNull(doctor.Appointments);
            Assert.Empty(doctor.Appointments);
            Assert.NotNull(doctor.TimeSlots);
            Assert.Empty(doctor.TimeSlots);
        }

        [Fact]
        public void DoctorID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            int expectedId = 10;

            // Act
            doctor.DoctorID = expectedId;

            // Assert
            Assert.Equal(expectedId, doctor.DoctorID);
        }

        [Fact]
        public void Name_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedName = "Dr. Emily Johnson";

            // Act
            doctor.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, doctor.Name);
        }

        [Fact]
        public void Name_ShouldAcceptEmptyString()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, doctor.Name);
        }

        [Fact]
        public void Email_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedEmail = "emily.johnson@clinic.com";

            // Act
            doctor.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, doctor.Email);
        }

        [Fact]
        public void Email_ShouldAcceptEmptyString()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Email = string.Empty;

            // Assert
            Assert.Equal(string.Empty, doctor.Email);
        }

        [Fact]
        public void Password_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedPassword = "SecurePassword456";

            // Act
            doctor.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, doctor.Password);
        }

        [Fact]
        public void Password_ShouldAcceptEmptyString()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Password = string.Empty;

            // Assert
            Assert.Equal(string.Empty, doctor.Password);
        }

        [Fact]
        public void BirthDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            DateTime expectedBirthDate = new DateTime(1980, 5, 10);

            // Act
            doctor.BirthDate = expectedBirthDate;

            // Assert
            Assert.Equal(expectedBirthDate, doctor.BirthDate);
        }

        [Fact]
        public void DepartmentID_ShouldAcceptNullValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.DepartmentID = null;

            // Assert
            Assert.Null(doctor.DepartmentID);
        }

        [Fact]
        public void DepartmentID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            int expectedDepartmentId = 5;

            // Act
            doctor.DepartmentID = expectedDepartmentId;

            // Assert
            Assert.Equal(expectedDepartmentId, doctor.DepartmentID);
        }

        [Fact]
        public void Phone_ShouldAcceptNullValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Phone = null;

            // Assert
            Assert.Null(doctor.Phone);
        }

        [Fact]
        public void Phone_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedPhone = "555-7890";

            // Act
            doctor.Phone = expectedPhone;

            // Assert
            Assert.Equal(expectedPhone, doctor.Phone);
        }

        [Fact]
        public void Address_ShouldAcceptNullValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Address = null;

            // Assert
            Assert.Null(doctor.Address);
        }

        [Fact]
        public void Address_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedAddress = "456 Medical Plaza";

            // Act
            doctor.Address = expectedAddress;

            // Assert
            Assert.Equal(expectedAddress, doctor.Address);
        }

        [Fact]
        public void Specialization_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            string expectedSpecialization = "Cardiology";

            // Act
            doctor.Specialization = expectedSpecialization;

            // Assert
            Assert.Equal(expectedSpecialization, doctor.Specialization);
        }

        [Fact]
        public void Specialization_ShouldAcceptEmptyString()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Specialization = string.Empty;

            // Assert
            Assert.Equal(string.Empty, doctor.Specialization);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            DateTime expectedCreatedDate = new DateTime(2024, 1, 1);

            // Act
            doctor.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, doctor.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.ModifiedDate = null;

            // Assert
            Assert.Null(doctor.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            doctor.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, doctor.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.IsActive = true;

            // Assert
            Assert.True(doctor.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.IsActive = false;

            // Assert
            Assert.False(doctor.IsActive);
        }

        [Fact]
        public void Department_ShouldAcceptNullValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.Department = null;

            // Assert
            Assert.Null(doctor.Department);
        }

        [Fact]
        public void Department_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var doctor = new Doctor();
            var department = new Department { DepartmentID = 1, Name = "Cardiology" };

            // Act
            doctor.Department = department;

            // Assert
            Assert.NotNull(doctor.Department);
            Assert.Equal(department, doctor.Department);
        }

        [Fact]
        public void Appointments_ShouldInitializeAsEmptyCollection()
        {
            // Arrange & Act
            var doctor = new Doctor();

            // Assert
            Assert.NotNull(doctor.Appointments);
            Assert.Empty(doctor.Appointments);
        }

        [Fact]
        public void Appointments_ShouldAllowAddingItems()
        {
            // Arrange
            var doctor = new Doctor();
            var appointment = new Appointment { AppointmentID = 1 };

            // Act
            doctor.Appointments.Add(appointment);

            // Assert
            Assert.Single(doctor.Appointments);
            Assert.Contains(appointment, doctor.Appointments);
        }

        [Fact]
        public void Appointments_ShouldAllowAddingMultipleItems()
        {
            // Arrange
            var doctor = new Doctor();
            var appointment1 = new Appointment { AppointmentID = 1 };
            var appointment2 = new Appointment { AppointmentID = 2 };

            // Act
            doctor.Appointments.Add(appointment1);
            doctor.Appointments.Add(appointment2);

            // Assert
            Assert.Equal(2, doctor.Appointments.Count);
            Assert.Contains(appointment1, doctor.Appointments);
            Assert.Contains(appointment2, doctor.Appointments);
        }

        [Fact]
        public void TimeSlots_ShouldInitializeAsEmptyCollection()
        {
            // Arrange & Act
            var doctor = new Doctor();

            // Assert
            Assert.NotNull(doctor.TimeSlots);
            Assert.Empty(doctor.TimeSlots);
        }

        [Fact]
        public void TimeSlots_ShouldAllowAddingItems()
        {
            // Arrange
            var doctor = new Doctor();
            var timeSlot = new TimeSlot { TimeSlotID = 1 };

            // Act
            doctor.TimeSlots.Add(timeSlot);

            // Assert
            Assert.Single(doctor.TimeSlots);
            Assert.Contains(timeSlot, doctor.TimeSlots);
        }

        [Fact]
        public void TimeSlots_ShouldAllowAddingMultipleItems()
        {
            // Arrange
            var doctor = new Doctor();
            var timeSlot1 = new TimeSlot { TimeSlotID = 1 };
            var timeSlot2 = new TimeSlot { TimeSlotID = 2 };

            // Act
            doctor.TimeSlots.Add(timeSlot1);
            doctor.TimeSlots.Add(timeSlot2);

            // Assert
            Assert.Equal(2, doctor.TimeSlots.Count);
            Assert.Contains(timeSlot1, doctor.TimeSlots);
            Assert.Contains(timeSlot2, doctor.TimeSlots);
        }

        [Fact]
        public void DoctorProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedBirthDate = new DateTime(1975, 3, 15);
            var expectedCreatedDate = new DateTime(2024, 1, 10);
            var expectedModifiedDate = new DateTime(2024, 2, 1);

            // Act
            var doctor = new Doctor
            {
                DoctorID = 25,
                Name = "Dr. Michael Brown",
                Email = "michael.brown@clinic.com",
                Password = "Password123",
                BirthDate = expectedBirthDate,
                DepartmentID = 3,
                Phone = "555-1111",
                Address = "789 Healthcare Drive",
                Specialization = "Neurology",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(25, doctor.DoctorID);
            Assert.Equal("Dr. Michael Brown", doctor.Name);
            Assert.Equal("michael.brown@clinic.com", doctor.Email);
            Assert.Equal("Password123", doctor.Password);
            Assert.Equal(expectedBirthDate, doctor.BirthDate);
            Assert.Equal(3, doctor.DepartmentID);
            Assert.Equal("555-1111", doctor.Phone);
            Assert.Equal("789 Healthcare Drive", doctor.Address);
            Assert.Equal("Neurology", doctor.Specialization);
            Assert.Equal(expectedCreatedDate, doctor.CreatedDate);
            Assert.Equal(expectedModifiedDate, doctor.ModifiedDate);
            Assert.True(doctor.IsActive);
        }

        [Fact]
        public void DoctorID_ShouldAcceptZeroValue()
        {
            // Arrange
            var doctor = new Doctor();

            // Act
            doctor.DoctorID = 0;

            // Assert
            Assert.Equal(0, doctor.DoctorID);
        }

        [Fact]
        public void Specialization_ShouldAcceptVariousValues()
        {
            // Arrange
            var doctor = new Doctor();
            string[] specializations = { "Cardiology", "Neurology", "Orthopedics", "Pediatrics" };

            foreach (var specialization in specializations)
            {
                // Act
                doctor.Specialization = specialization;

                // Assert
                Assert.Equal(specialization, doctor.Specialization);
            }
        }

        [Fact]
        public void Phone_ShouldAcceptVariousPhoneFormats()
        {
            // Arrange
            var doctor = new Doctor();
            string[] phoneNumbers = { "555-1234", "(555) 123-4567", "555.123.4567" };

            foreach (var phone in phoneNumbers)
            {
                // Act
                doctor.Phone = phone;

                // Assert
                Assert.Equal(phone, doctor.Phone);
            }
        }
    }
}
