using Xunit;
using ClinicManagement.Application.DTOs;
using System;

namespace ClinicManagement.Application.Tests
{
    public class PatientDtoTests
    {
        [Fact]
        public void PatientDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new PatientDto
            {
                Id = 1,
                Name = "John Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Email = "john.doe@email.com",
                PhoneNumber = "+1234567890",
                Gender = "Male",
                Address = "123 Main St",
                IsActive = true
            };

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("John Doe", dto.Name);
            Assert.Equal(new DateTime(1990, 1, 1), dto.BirthDate);
            Assert.Equal("john.doe@email.com", dto.Email);
            Assert.Equal("+1234567890", dto.PhoneNumber);
            Assert.Equal("Male", dto.Gender);
            Assert.Equal("123 Main St", dto.Address);
            Assert.True(dto.IsActive);
        }

        [Fact]
        public void PatientDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new PatientDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.Email);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Gender);
            Assert.Equal(string.Empty, dto.Address);
            Assert.False(dto.IsActive);
        }

        [Fact]
        public void PatientCreateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new PatientCreateDto
            {
                Name = "Jane Smith",
                BirthDate = new DateTime(1985, 5, 15),
                Email = "jane.smith@email.com",
                Password = "SecurePass123",
                PhoneNumber = "+9876543210",
                Gender = "Female",
                Address = "456 Elm St"
            };

            // Assert
            Assert.Equal("Jane Smith", dto.Name);
            Assert.Equal(new DateTime(1985, 5, 15), dto.BirthDate);
            Assert.Equal("jane.smith@email.com", dto.Email);
            Assert.Equal("SecurePass123", dto.Password);
            Assert.Equal("+9876543210", dto.PhoneNumber);
            Assert.Equal("Female", dto.Gender);
            Assert.Equal("456 Elm St", dto.Address);
        }

        [Fact]
        public void PatientCreateDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new PatientCreateDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.Email);
            Assert.Equal(string.Empty, dto.Password);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Gender);
            Assert.Equal(string.Empty, dto.Address);
        }

        [Fact]
        public void PatientUpdateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new PatientUpdateDto
            {
                Name = "Updated Name",
                PhoneNumber = "+1111111111",
                Address = "789 Oak St"
            };

            // Assert
            Assert.Equal("Updated Name", dto.Name);
            Assert.Equal("+1111111111", dto.PhoneNumber);
            Assert.Equal("789 Oak St", dto.Address);
        }

        [Fact]
        public void PatientUpdateDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new PatientUpdateDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Address);
        }

        [Fact]
        public void PatientDto_Name_ShouldHandleEmptyString()
        {
            // Arrange
            var dto = new PatientDto();

            // Act
            dto.Name = "";

            // Assert
            Assert.Equal(string.Empty, dto.Name);
        }

        [Fact]
        public void PatientCreateDto_Password_ShouldSetValue()
        {
            // Arrange
            var dto = new PatientCreateDto();
            var expectedPassword = "TestPassword123";

            // Act
            dto.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, dto.Password);
        }
    }
}
