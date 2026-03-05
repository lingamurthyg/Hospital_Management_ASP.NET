using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class PatientTests
{
    [Fact]
    public void Patient_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var patient = new Patient();

        // Assert
        Assert.Equal(0, patient.Id);
        Assert.Equal(0, patient.UserId);
        Assert.Null(patient.MedicalHistory);
        Assert.False(patient.IsActive);
        Assert.Equal(string.Empty, patient.CreatedBy);
        Assert.Null(patient.ModifiedBy);
        Assert.Null(patient.User);
        Assert.NotNull(patient.Appointments);
        Assert.Empty(patient.Appointments);
        Assert.NotNull(patient.Bills);
        Assert.Empty(patient.Bills);
        Assert.NotNull(patient.Feedbacks);
        Assert.Empty(patient.Feedbacks);
    }

    [Fact]
    public void Patient_ShouldSet_IdProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Id = 1;

        // Assert
        Assert.Equal(1, patient.Id);
    }

    [Fact]
    public void Patient_ShouldSet_UserIdProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UserId = 10;

        // Assert
        Assert.Equal(10, patient.UserId);
    }

    [Fact]
    public void Patient_ShouldSet_MedicalHistoryProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.MedicalHistory = "Diabetes, Hypertension";

        // Assert
        Assert.Equal("Diabetes, Hypertension", patient.MedicalHistory);
    }

    [Fact]
    public void Patient_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.IsActive = true;

        // Assert
        Assert.True(patient.IsActive);
    }

    [Fact]
    public void Patient_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var patient = new Patient();
        var date = DateTime.UtcNow;

        // Act
        patient.CreatedDate = date;

        // Assert
        Assert.Equal(date, patient.CreatedDate);
    }

    [Fact]
    public void Patient_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var patient = new Patient();
        var date = DateTime.UtcNow;

        // Act
        patient.ModifiedDate = date;

        // Assert
        Assert.Equal(date, patient.ModifiedDate);
    }

    [Fact]
    public void Patient_ShouldSet_UserProperty()
    {
        // Arrange
        var patient = new Patient();
        var user = new User { Id = 1, Name = "John Patient" };

        // Act
        patient.User = user;

        // Assert
        Assert.NotNull(patient.User);
        Assert.Equal(1, patient.User.Id);
        Assert.Equal("John Patient", patient.User.Name);
    }

    [Fact]
    public void Patient_ShouldSet_AppointmentsCollection()
    {
        // Arrange
        var patient = new Patient();
        var appointments = new List<Appointment>
        {
            new Appointment { Id = 1, PatientId = 1 },
            new Appointment { Id = 2, PatientId = 1 }
        };

        // Act
        patient.Appointments = appointments;

        // Assert
        Assert.NotNull(patient.Appointments);
        Assert.Equal(2, patient.Appointments.Count);
    }

    [Fact]
    public void Patient_ShouldSet_BillsCollection()
    {
        // Arrange
        var patient = new Patient();
        var bills = new List<Bill>
        {
            new Bill { Id = 1, PatientId = 1 },
            new Bill { Id = 2, PatientId = 1 }
        };

        // Act
        patient.Bills = bills;

        // Assert
        Assert.NotNull(patient.Bills);
        Assert.Equal(2, patient.Bills.Count);
    }

    [Fact]
    public void Patient_ShouldSet_FeedbacksCollection()
    {
        // Arrange
        var patient = new Patient();
        var feedbacks = new List<Feedback>
        {
            new Feedback { Id = 1, PatientId = 1 },
            new Feedback { Id = 2, PatientId = 1 }
        };

        // Act
        patient.Feedbacks = feedbacks;

        // Assert
        Assert.NotNull(patient.Feedbacks);
        Assert.Equal(2, patient.Feedbacks.Count);
    }

    [Fact]
    public void Patient_MedicalHistory_ShouldAcceptNull()
    {
        // Arrange & Act
        var patient = new Patient { MedicalHistory = null };

        // Assert
        Assert.Null(patient.MedicalHistory);
    }

    [Fact]
    public void Patient_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var user = new User { Id = 1 };

        // Act
        var patient = new Patient
        {
            Id = 5,
            UserId = 1,
            MedicalHistory = "Asthma",
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Admin",
            User = user
        };

        // Assert
        Assert.Equal(5, patient.Id);
        Assert.Equal(1, patient.UserId);
        Assert.Equal("Asthma", patient.MedicalHistory);
        Assert.Equal(date, patient.CreatedDate);
        Assert.Equal(date, patient.ModifiedDate);
        Assert.True(patient.IsActive);
        Assert.Equal("System", patient.CreatedBy);
        Assert.Equal("Admin", patient.ModifiedBy);
        Assert.NotNull(patient.User);
    }
}
