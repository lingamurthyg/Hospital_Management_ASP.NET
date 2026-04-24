using Xunit;
using ClinicManagement.Application.DTOs;
using System;

namespace ClinicManagement.Application.Tests
{
    public class DoctorDtoTests
    {
        [Fact]
        public void DoctorDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new DoctorDto
            {
                Id = 1,
                Name = "Dr. John Smith",
                Email = "john.smith@clinic.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890",
                Address = "123 Medical Plaza",
                IsActive = true
            };

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("Dr. John Smith", dto.Name);
            Assert.Equal("john.smith@clinic.com", dto.Email);
            Assert.Equal("Cardiology", dto.Specialization);
            Assert.Equal("+1234567890", dto.PhoneNumber);
            Assert.Equal("123 Medical Plaza", dto.Address);
            Assert.True(dto.IsActive);
        }

        [Fact]
        public void DoctorDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new DoctorDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.Email);
            Assert.Equal(string.Empty, dto.Specialization);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Address);
            Assert.False(dto.IsActive);
        }

        [Fact]
        public void DoctorCreateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new DoctorCreateDto
            {
                Name = "Dr. Jane Doe",
                Email = "jane.doe@clinic.com",
                Password = "SecurePass123",
                Specialization = "Neurology",
                PhoneNumber = "+9876543210",
                Address = "456 Hospital Rd"
            };

            // Assert
            Assert.Equal("Dr. Jane Doe", dto.Name);
            Assert.Equal("jane.doe@clinic.com", dto.Email);
            Assert.Equal("SecurePass123", dto.Password);
            Assert.Equal("Neurology", dto.Specialization);
            Assert.Equal("+9876543210", dto.PhoneNumber);
            Assert.Equal("456 Hospital Rd", dto.Address);
        }

        [Fact]
        public void DoctorCreateDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new DoctorCreateDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.Email);
            Assert.Equal(string.Empty, dto.Password);
            Assert.Equal(string.Empty, dto.Specialization);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Address);
        }

        [Fact]
        public void DoctorUpdateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new DoctorUpdateDto
            {
                Name = "Dr. Updated Name",
                Specialization = "Orthopedics",
                PhoneNumber = "+1111111111",
                Address = "789 Clinic Ave"
            };

            // Assert
            Assert.Equal("Dr. Updated Name", dto.Name);
            Assert.Equal("Orthopedics", dto.Specialization);
            Assert.Equal("+1111111111", dto.PhoneNumber);
            Assert.Equal("789 Clinic Ave", dto.Address);
        }

        [Fact]
        public void DoctorUpdateDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new DoctorUpdateDto();

            // Assert
            Assert.Equal(string.Empty, dto.Name);
            Assert.Equal(string.Empty, dto.Specialization);
            Assert.Equal(string.Empty, dto.PhoneNumber);
            Assert.Equal(string.Empty, dto.Address);
        }

        [Fact]
        public void DoctorDto_Specialization_MultipleValues_ShouldBeSettable()
        {
            // Arrange
            var dto = new DoctorDto();

            // Act & Assert - Cardiology
            dto.Specialization = "Cardiology";
            Assert.Equal("Cardiology", dto.Specialization);

            // Act & Assert - Neurology
            dto.Specialization = "Neurology";
            Assert.Equal("Neurology", dto.Specialization);

            // Act & Assert - Orthopedics
            dto.Specialization = "Orthopedics";
            Assert.Equal("Orthopedics", dto.Specialization);
        }

        [Fact]
        public void DoctorCreateDto_Password_ShouldSetValue()
        {
            // Arrange
            var dto = new DoctorCreateDto();
            var expectedPassword = "TestPassword456";

            // Act
            dto.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, dto.Password);
        }

        [Fact]
        public void DoctorDto_IsActive_ShouldToggle()
        {
            // Arrange
            var dto = new DoctorDto();

            // Act
            dto.IsActive = true;
            Assert.True(dto.IsActive);

            dto.IsActive = false;
            Assert.False(dto.IsActive);
        }

        [Fact]
        public void DoctorDto_Email_ShouldHandleEmptyString()
        {
            // Arrange
            var dto = new DoctorDto();

            // Act
            dto.Email = "";

            // Assert
            Assert.Equal(string.Empty, dto.Email);
        }
    }
}
