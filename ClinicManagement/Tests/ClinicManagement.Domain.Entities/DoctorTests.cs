using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class DoctorTests
{
    [Fact]
    public void Doctor_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var doctor = new Doctor();

        // Assert
        Assert.Equal(0, doctor.Id);
        Assert.Equal(0, doctor.UserId);
        Assert.Equal(string.Empty, doctor.Specialization);
        Assert.Null(doctor.Qualification);
        Assert.Null(doctor.ConsultationFee);
        Assert.Null(doctor.AvailableTimings);
        Assert.False(doctor.IsActive);
        Assert.Equal(string.Empty, doctor.CreatedBy);
        Assert.Null(doctor.ModifiedBy);
        Assert.Null(doctor.User);
        Assert.NotNull(doctor.Appointments);
        Assert.Empty(doctor.Appointments);
    }

    [Fact]
    public void Doctor_ShouldSet_IdProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.Id = 100;

        // Assert
        Assert.Equal(100, doctor.Id);
    }

    [Fact]
    public void Doctor_ShouldSet_UserIdProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.UserId = 50;

        // Assert
        Assert.Equal(50, doctor.UserId);
    }

    [Fact]
    public void Doctor_ShouldSet_SpecializationProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.Specialization = "Cardiology";

        // Assert
        Assert.Equal("Cardiology", doctor.Specialization);
    }

    [Fact]
    public void Doctor_ShouldSet_QualificationProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.Qualification = "MD";

        // Assert
        Assert.Equal("MD", doctor.Qualification);
    }

    [Fact]
    public void Doctor_ShouldSet_ConsultationFeeProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.ConsultationFee = 150.50m;

        // Assert
        Assert.Equal(150.50m, doctor.ConsultationFee);
    }

    [Fact]
    public void Doctor_ShouldSet_AvailableTimingsProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.AvailableTimings = "9 AM - 5 PM";

        // Assert
        Assert.Equal("9 AM - 5 PM", doctor.AvailableTimings);
    }

    [Fact]
    public void Doctor_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.IsActive = true;

        // Assert
        Assert.True(doctor.IsActive);
    }

    [Fact]
    public void Doctor_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var doctor = new Doctor();
        var date = DateTime.UtcNow;

        // Act
        doctor.CreatedDate = date;

        // Assert
        Assert.Equal(date, doctor.CreatedDate);
    }

    [Fact]
    public void Doctor_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var doctor = new Doctor();
        var date = DateTime.UtcNow;

        // Act
        doctor.ModifiedDate = date;

        // Assert
        Assert.Equal(date, doctor.ModifiedDate);
    }

    [Fact]
    public void Doctor_ShouldSet_UserProperty()
    {
        // Arrange
        var doctor = new Doctor();
        var user = new User { Id = 1, Name = "Dr. Smith" };

        // Act
        doctor.User = user;

        // Assert
        Assert.NotNull(doctor.User);
        Assert.Equal(1, doctor.User.Id);
        Assert.Equal("Dr. Smith", doctor.User.Name);
    }

    [Fact]
    public void Doctor_ShouldSet_AppointmentsCollection()
    {
        // Arrange
        var doctor = new Doctor();
        var appointments = new List<Appointment>
        {
            new Appointment { Id = 1, DoctorId = 1 },
            new Appointment { Id = 2, DoctorId = 1 }
        };

        // Act
        doctor.Appointments = appointments;

        // Assert
        Assert.NotNull(doctor.Appointments);
        Assert.Equal(2, doctor.Appointments.Count);
    }

    [Fact]
    public void Doctor_ConsultationFee_ShouldAcceptNull()
    {
        // Arrange & Act
        var doctor = new Doctor { ConsultationFee = null };

        // Assert
        Assert.Null(doctor.ConsultationFee);
    }

    [Fact]
    public void Doctor_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var user = new User { Id = 1 };

        // Act
        var doctor = new Doctor
        {
            Id = 10,
            UserId = 1,
            Specialization = "Neurology",
            Qualification = "MBBS, MD",
            ConsultationFee = 200.00m,
            AvailableTimings = "10 AM - 6 PM",
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "Admin",
            ModifiedBy = "Admin",
            User = user
        };

        // Assert
        Assert.Equal(10, doctor.Id);
        Assert.Equal(1, doctor.UserId);
        Assert.Equal("Neurology", doctor.Specialization);
        Assert.Equal("MBBS, MD", doctor.Qualification);
        Assert.Equal(200.00m, doctor.ConsultationFee);
        Assert.Equal("10 AM - 6 PM", doctor.AvailableTimings);
        Assert.Equal(date, doctor.CreatedDate);
        Assert.Equal(date, doctor.ModifiedDate);
        Assert.True(doctor.IsActive);
        Assert.Equal("Admin", doctor.CreatedBy);
        Assert.Equal("Admin", doctor.ModifiedBy);
        Assert.NotNull(doctor.User);
    }
}
