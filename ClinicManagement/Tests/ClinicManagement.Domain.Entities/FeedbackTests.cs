using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class FeedbackTests
{
    [Fact]
    public void Feedback_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var feedback = new Feedback();

        // Assert
        Assert.Equal(0, feedback.Id);
        Assert.Equal(0, feedback.PatientId);
        Assert.Equal(string.Empty, feedback.Message);
        Assert.Null(feedback.Rating);
        Assert.Equal(DateTime.MinValue, feedback.FeedbackDate);
        Assert.False(feedback.IsActive);
        Assert.Equal(string.Empty, feedback.CreatedBy);
        Assert.Null(feedback.ModifiedBy);
        Assert.Null(feedback.Patient);
    }

    [Fact]
    public void Feedback_ShouldSet_IdProperty()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Id = 1;

        // Assert
        Assert.Equal(1, feedback.Id);
    }

    [Fact]
    public void Feedback_ShouldSet_PatientIdProperty()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.PatientId = 10;

        // Assert
        Assert.Equal(10, feedback.PatientId);
    }

    [Fact]
    public void Feedback_ShouldSet_MessageProperty()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Message = "Great service!";

        // Assert
        Assert.Equal("Great service!", feedback.Message);
    }

    [Fact]
    public void Feedback_ShouldSet_RatingProperty()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Rating = 5;

        // Assert
        Assert.Equal(5, feedback.Rating);
    }

    [Fact]
    public void Feedback_ShouldSet_FeedbackDateProperty()
    {
        // Arrange
        var feedback = new Feedback();
        var date = DateTime.UtcNow;

        // Act
        feedback.FeedbackDate = date;

        // Assert
        Assert.Equal(date, feedback.FeedbackDate);
    }

    [Fact]
    public void Feedback_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.IsActive = true;

        // Assert
        Assert.True(feedback.IsActive);
    }

    [Fact]
    public void Feedback_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var feedback = new Feedback();
        var date = DateTime.UtcNow;

        // Act
        feedback.CreatedDate = date;

        // Assert
        Assert.Equal(date, feedback.CreatedDate);
    }

    [Fact]
    public void Feedback_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var feedback = new Feedback();
        var date = DateTime.UtcNow;

        // Act
        feedback.ModifiedDate = date;

        // Assert
        Assert.Equal(date, feedback.ModifiedDate);
    }

    [Fact]
    public void Feedback_ShouldSet_PatientProperty()
    {
        // Arrange
        var feedback = new Feedback();
        var patient = new Patient { Id = 1, UserId = 5 };

        // Act
        feedback.Patient = patient;

        // Assert
        Assert.NotNull(feedback.Patient);
        Assert.Equal(1, feedback.Patient.Id);
    }

    [Fact]
    public void Feedback_Rating_ShouldAcceptNull()
    {
        // Arrange & Act
        var feedback = new Feedback { Rating = null };

        // Assert
        Assert.Null(feedback.Rating);
    }

    [Fact]
    public void Feedback_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var patient = new Patient { Id = 1 };

        // Act
        var feedback = new Feedback
        {
            Id = 3,
            PatientId = 1,
            Message = "Excellent care",
            Rating = 4,
            FeedbackDate = date,
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Admin",
            Patient = patient
        };

        // Assert
        Assert.Equal(3, feedback.Id);
        Assert.Equal(1, feedback.PatientId);
        Assert.Equal("Excellent care", feedback.Message);
        Assert.Equal(4, feedback.Rating);
        Assert.Equal(date, feedback.FeedbackDate);
        Assert.Equal(date, feedback.CreatedDate);
        Assert.Equal(date, feedback.ModifiedDate);
        Assert.True(feedback.IsActive);
        Assert.Equal("System", feedback.CreatedBy);
        Assert.Equal("Admin", feedback.ModifiedBy);
        Assert.NotNull(feedback.Patient);
    }

    [Fact]
    public void Feedback_Message_ShouldAcceptEmptyString()
    {
        // Arrange & Act
        var feedback = new Feedback { Message = string.Empty };

        // Assert
        Assert.Equal(string.Empty, feedback.Message);
    }

    [Fact]
    public void Feedback_Rating_ShouldAcceptValidRange()
    {
        // Arrange
        var feedback = new Feedback();

        // Act & Assert
        feedback.Rating = 1;
        Assert.Equal(1, feedback.Rating);

        feedback.Rating = 3;
        Assert.Equal(3, feedback.Rating);

        feedback.Rating = 5;
        Assert.Equal(5, feedback.Rating);
    }
}
