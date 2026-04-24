using Xunit;
using ClinicManagement.Application.DTOs;
using System;

namespace ClinicManagement.Application.Tests
{
    public class AppointmentDtoTests
    {
        [Fact]
        public void AppointmentDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new AppointmentDto
            {
                Id = 1,
                PatientId = 10,
                PatientName = "John Doe",
                DoctorId = 5,
                DoctorName = "Dr. Smith",
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Scheduled",
                Notes = "Regular checkup"
            };

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal(10, dto.PatientId);
            Assert.Equal("John Doe", dto.PatientName);
            Assert.Equal(5, dto.DoctorId);
            Assert.Equal("Dr. Smith", dto.DoctorName);
            Assert.Equal("Scheduled", dto.Status);
            Assert.Equal("Regular checkup", dto.Notes);
        }

        [Fact]
        public void AppointmentDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new AppointmentDto();

            // Assert
            Assert.Equal(string.Empty, dto.PatientName);
            Assert.Equal(string.Empty, dto.DoctorName);
            Assert.Equal(string.Empty, dto.Status);
        }

        [Fact]
        public void AppointmentDto_Notes_CanBeNull()
        {
            // Arrange
            var dto = new AppointmentDto();

            // Act
            dto.Notes = null;

            // Assert
            Assert.Null(dto.Notes);
        }

        [Fact]
        public void AppointmentCreateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new AppointmentCreateDto
            {
                PatientId = 10,
                DoctorId = 5,
                AppointmentDate = DateTime.UtcNow.AddDays(2),
                Notes = "Follow-up appointment"
            };

            // Assert
            Assert.Equal(10, dto.PatientId);
            Assert.Equal(5, dto.DoctorId);
            Assert.Equal("Follow-up appointment", dto.Notes);
        }

        [Fact]
        public void AppointmentCreateDto_Notes_CanBeNull()
        {
            // Arrange
            var dto = new AppointmentCreateDto
            {
                PatientId = 10,
                DoctorId = 5,
                AppointmentDate = DateTime.UtcNow.AddDays(2)
            };

            // Act
            dto.Notes = null;

            // Assert
            Assert.Null(dto.Notes);
        }

        [Fact]
        public void AppointmentUpdateDto_AllProperties_ShouldSetAndGetValues()
        {
            // Arrange & Act
            var dto = new AppointmentUpdateDto
            {
                AppointmentDate = DateTime.UtcNow.AddDays(3),
                Status = "Confirmed",
                Notes = "Patient confirmed attendance"
            };

            // Assert
            Assert.Equal("Confirmed", dto.Status);
            Assert.Equal("Patient confirmed attendance", dto.Notes);
        }

        [Fact]
        public void AppointmentUpdateDto_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var dto = new AppointmentUpdateDto();

            // Assert
            Assert.Equal(string.Empty, dto.Status);
        }

        [Fact]
        public void AppointmentUpdateDto_Notes_CanBeNull()
        {
            // Arrange
            var dto = new AppointmentUpdateDto
            {
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending"
            };

            // Act
            dto.Notes = null;

            // Assert
            Assert.Null(dto.Notes);
        }

        [Fact]
        public void AppointmentDto_Status_MultipleValues_ShouldBeSettable()
        {
            // Arrange
            var dto = new AppointmentDto();

            // Act & Assert - Scheduled
            dto.Status = "Scheduled";
            Assert.Equal("Scheduled", dto.Status);

            // Act & Assert - Confirmed
            dto.Status = "Confirmed";
            Assert.Equal("Confirmed", dto.Status);

            // Act & Assert - Cancelled
            dto.Status = "Cancelled";
            Assert.Equal("Cancelled", dto.Status);

            // Act & Assert - Completed
            dto.Status = "Completed";
            Assert.Equal("Completed", dto.Status);
        }

        [Fact]
        public void AppointmentCreateDto_PatientId_ShouldSetZero()
        {
            // Arrange
            var dto = new AppointmentCreateDto();

            // Act
            dto.PatientId = 0;

            // Assert
            Assert.Equal(0, dto.PatientId);
        }

        [Fact]
        public void AppointmentCreateDto_DoctorId_ShouldSetZero()
        {
            // Arrange
            var dto = new AppointmentCreateDto();

            // Act
            dto.DoctorId = 0;

            // Assert
            Assert.Equal(0, dto.DoctorId);
        }
    }
}
