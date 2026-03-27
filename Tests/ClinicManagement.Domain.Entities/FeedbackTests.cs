using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests;

public class FeedbackTests
{
    [Fact]
    public void Feedback_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var feedback = new Feedback();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.NotNull(feedback);
        Assert.InRange(feedback.FeedbackDate, beforeCreation, afterCreation);
    }

    [Fact]
    public void Feedback_FeedbackDate_DefaultShouldBeUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var feedback = new Feedback();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.InRange(feedback.FeedbackDate, beforeCreation, afterCreation);
    }

    [Fact]
    public void Feedback_SetAppointmentId_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.AppointmentId = 50;

        // Assert
        Assert.Equal(50, feedback.AppointmentId);
    }

    [Fact]
    public void Feedback_SetPatientId_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.PatientId = 100;

        // Assert
        Assert.Equal(100, feedback.PatientId);
    }

    [Fact]
    public void Feedback_SetDoctorId_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.DoctorId = 25;

        // Assert
        Assert.Equal(25, feedback.DoctorId);
    }

    [Fact]
    public void Feedback_SetRating_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Rating = 5;

        // Assert
        Assert.Equal(5, feedback.Rating);
    }

    [Fact]
    public void Feedback_SetComments_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Comments = "Excellent service!";

        // Assert
        Assert.Equal("Excellent service!", feedback.Comments);
    }

    [Fact]
    public void Feedback_SetFeedbackDate_ShouldUpdateValue()
    {
        // Arrange
        var feedback = new Feedback();
        var customDate = new DateTime(2024, 5, 10, 14, 30, 0);

        // Act
        feedback.FeedbackDate = customDate;

        // Assert
        Assert.Equal(customDate, feedback.FeedbackDate);
    }

    [Fact]
    public void Feedback_Comments_ShouldAcceptNull()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Comments = null;

        // Assert
        Assert.Null(feedback.Comments);
    }

    [Fact]
    public void Feedback_NavigationProperties_ShouldAcceptNull()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Appointment = null;
        feedback.Patient = null;

        // Assert
        Assert.Null(feedback.Appointment);
        Assert.Null(feedback.Patient);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Feedback_Rating_ShouldAcceptVariousValues(int rating)
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Rating = rating;

        // Assert
        Assert.Equal(rating, feedback.Rating);
    }

    [Fact]
    public void Feedback_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var feedback = new Feedback();
        var date = new DateTime(2024, 3, 15, 10, 0, 0);

        // Act
        feedback.AppointmentId = 10;
        feedback.PatientId = 20;
        feedback.DoctorId = 30;
        feedback.Rating = 4;
        feedback.Comments = "Good experience";
        feedback.FeedbackDate = date;

        // Assert
        Assert.Equal(10, feedback.AppointmentId);
        Assert.Equal(20, feedback.PatientId);
        Assert.Equal(30, feedback.DoctorId);
        Assert.Equal(4, feedback.Rating);
        Assert.Equal("Good experience", feedback.Comments);
        Assert.Equal(date, feedback.FeedbackDate);
    }

    [Fact]
    public void Feedback_Rating_ShouldAcceptZero()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Rating = 0;

        // Assert
        Assert.Equal(0, feedback.Rating);
    }

    [Fact]
    public void Feedback_Rating_ShouldAcceptNegativeValues()
    {
        // Arrange
        var feedback = new Feedback();

        // Act
        feedback.Rating = -1;

        // Assert
        Assert.Equal(-1, feedback.Rating);
    }
}
