using Xunit;
using System;
using System.Collections.Generic;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class DepartmentTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDepartmentWithDefaultValues()
        {
            // Arrange & Act
            var department = new Department();

            // Assert
            Assert.Equal(0, department.DepartmentID);
            Assert.Equal(string.Empty, department.Name);
            Assert.Null(department.Description);
            Assert.Equal(default(DateTime), department.CreatedDate);
            Assert.Null(department.ModifiedDate);
            Assert.False(department.IsActive);
            Assert.NotNull(department.Doctors);
            Assert.Empty(department.Doctors);
        }

        [Fact]
        public void DepartmentID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var department = new Department();
            int expectedId = 42;

            // Act
            department.DepartmentID = expectedId;

            // Assert
            Assert.Equal(expectedId, department.DepartmentID);
        }

        [Fact]
        public void Name_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var department = new Department();
            string expectedName = "Cardiology";

            // Act
            department.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, department.Name);
        }

        [Fact]
        public void Name_ShouldAcceptEmptyString()
        {
            // Arrange
            var department = new Department();

            // Act
            department.Name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, department.Name);
        }

        [Fact]
        public void Description_ShouldAcceptNullValue()
        {
            // Arrange
            var department = new Department();

            // Act
            department.Description = null;

            // Assert
            Assert.Null(department.Description);
        }

        [Fact]
        public void Description_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var department = new Department();
            string expectedDescription = "Department specializing in heart conditions";

            // Act
            department.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, department.Description);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var department = new Department();
            DateTime expectedCreatedDate = new DateTime(2024, 1, 1);

            // Act
            department.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, department.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var department = new Department();

            // Act
            department.ModifiedDate = null;

            // Assert
            Assert.Null(department.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var department = new Department();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            department.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, department.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var department = new Department();

            // Act
            department.IsActive = true;

            // Assert
            Assert.True(department.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var department = new Department();

            // Act
            department.IsActive = false;

            // Assert
            Assert.False(department.IsActive);
        }

        [Fact]
        public void Doctors_ShouldInitializeAsEmptyCollection()
        {
            // Arrange & Act
            var department = new Department();

            // Assert
            Assert.NotNull(department.Doctors);
            Assert.Empty(department.Doctors);
        }

        [Fact]
        public void Doctors_ShouldAllowAddingItems()
        {
            // Arrange
            var department = new Department();
            var doctor = new Doctor { DoctorID = 1, Name = "Dr. Smith" };

            // Act
            department.Doctors.Add(doctor);

            // Assert
            Assert.Single(department.Doctors);
            Assert.Contains(doctor, department.Doctors);
        }

        [Fact]
        public void Doctors_ShouldAllowAddingMultipleItems()
        {
            // Arrange
            var department = new Department();
            var doctor1 = new Doctor { DoctorID = 1, Name = "Dr. Smith" };
            var doctor2 = new Doctor { DoctorID = 2, Name = "Dr. Jones" };

            // Act
            department.Doctors.Add(doctor1);
            department.Doctors.Add(doctor2);

            // Assert
            Assert.Equal(2, department.Doctors.Count);
            Assert.Contains(doctor1, department.Doctors);
            Assert.Contains(doctor2, department.Doctors);
        }

        [Fact]
        public void DepartmentProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedCreatedDate = new DateTime(2024, 1, 15);
            var expectedModifiedDate = new DateTime(2024, 2, 1);

            // Act
            var department = new Department
            {
                DepartmentID = 10,
                Name = "Neurology",
                Description = "Brain and nervous system treatment",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(10, department.DepartmentID);
            Assert.Equal("Neurology", department.Name);
            Assert.Equal("Brain and nervous system treatment", department.Description);
            Assert.Equal(expectedCreatedDate, department.CreatedDate);
            Assert.Equal(expectedModifiedDate, department.ModifiedDate);
            Assert.True(department.IsActive);
        }

        [Fact]
        public void DepartmentID_ShouldAcceptZeroValue()
        {
            // Arrange
            var department = new Department();

            // Act
            department.DepartmentID = 0;

            // Assert
            Assert.Equal(0, department.DepartmentID);
        }

        [Fact]
        public void DepartmentID_ShouldAcceptNegativeValue()
        {
            // Arrange
            var department = new Department();

            // Act
            department.DepartmentID = -1;

            // Assert
            Assert.Equal(-1, department.DepartmentID);
        }

        [Fact]
        public void Description_ShouldAcceptLongText()
        {
            // Arrange
            var department = new Department();
            string longDescription = new string('A', 1000);

            // Act
            department.Description = longDescription;

            // Assert
            Assert.Equal(longDescription, department.Description);
        }
    }
}
