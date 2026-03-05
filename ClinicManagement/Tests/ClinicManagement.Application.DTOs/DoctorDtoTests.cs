using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class DoctorDtoTests
{
    [Fact]
    public void DoctorDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var doctorDto = new DoctorDto();

        // Assert
        Assert.Equal(0, doctorDto.Id);
        Assert.Equal(0, doctorDto.UserId);
        Assert.Equal(string.Empty, doctorDto.Specialization);
        Assert.Null(doctorDto.Qualification);
        Assert.Null(doctorDto.ConsultationFee);
        Assert.Null(doctorDto.AvailableTimings);
        Assert.Null(doctorDto.UserName);
        Assert.Null(doctorDto.UserEmail);
    }

    [Fact]
    public void DoctorDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var doctorDto = new DoctorDto
        {
            Id = 1,
            UserId = 10,
            Specialization = "Cardiology",
            Qualification = "MD",
            ConsultationFee = 150.00m,
            AvailableTimings = "9 AM - 5 PM",
            UserName = "Dr. Smith",
            UserEmail = "doctor@example.com"
        };

        // Assert
        Assert.Equal(1, doctorDto.Id);
        Assert.Equal(10, doctorDto.UserId);
        Assert.Equal("Cardiology", doctorDto.Specialization);
        Assert.Equal("MD", doctorDto.Qualification);
        Assert.Equal(150.00m, doctorDto.ConsultationFee);
        Assert.Equal("9 AM - 5 PM", doctorDto.AvailableTimings);
        Assert.Equal("Dr. Smith", doctorDto.UserName);
        Assert.Equal("doctor@example.com", doctorDto.UserEmail);
    }

    [Fact]
    public void DoctorCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var doctorCreateDto = new DoctorCreateDto();

        // Assert
        Assert.Equal(0, doctorCreateDto.UserId);
        Assert.Equal(string.Empty, doctorCreateDto.Specialization);
        Assert.Null(doctorCreateDto.Qualification);
        Assert.Null(doctorCreateDto.ConsultationFee);
        Assert.Null(doctorCreateDto.AvailableTimings);
    }

    [Fact]
    public void DoctorCreateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var doctorCreateDto = new DoctorCreateDto
        {
            UserId = 5,
            Specialization = "Neurology",
            Qualification = "MBBS, MD",
            ConsultationFee = 200.00m,
            AvailableTimings = "10 AM - 6 PM"
        };

        // Assert
        Assert.Equal(5, doctorCreateDto.UserId);
        Assert.Equal("Neurology", doctorCreateDto.Specialization);
        Assert.Equal("MBBS, MD", doctorCreateDto.Qualification);
        Assert.Equal(200.00m, doctorCreateDto.ConsultationFee);
        Assert.Equal("10 AM - 6 PM", doctorCreateDto.AvailableTimings);
    }

    [Fact]
    public void DoctorUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var doctorUpdateDto = new DoctorUpdateDto();

        // Assert
        Assert.Equal(string.Empty, doctorUpdateDto.Specialization);
        Assert.Null(doctorUpdateDto.Qualification);
        Assert.Null(doctorUpdateDto.ConsultationFee);
        Assert.Null(doctorUpdateDto.AvailableTimings);
    }

    [Fact]
    public void DoctorUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var doctorUpdateDto = new DoctorUpdateDto
        {
            Specialization = "Pediatrics",
            Qualification = "MD, Pediatrics",
            ConsultationFee = 175.50m,
            AvailableTimings = "8 AM - 4 PM"
        };

        // Assert
        Assert.Equal("Pediatrics", doctorUpdateDto.Specialization);
        Assert.Equal("MD, Pediatrics", doctorUpdateDto.Qualification);
        Assert.Equal(175.50m, doctorUpdateDto.ConsultationFee);
        Assert.Equal("8 AM - 4 PM", doctorUpdateDto.AvailableTimings);
    }

    [Fact]
    public void DoctorDto_ConsultationFee_ShouldAcceptNull()
    {
        // Arrange & Act
        var doctorDto = new DoctorDto { ConsultationFee = null };

        // Assert
        Assert.Null(doctorDto.ConsultationFee);
    }

    [Fact]
    public void DoctorDto_Qualification_ShouldAcceptNull()
    {
        // Arrange & Act
        var doctorDto = new DoctorDto { Qualification = null };

        // Assert
        Assert.Null(doctorDto.Qualification);
    }
}
