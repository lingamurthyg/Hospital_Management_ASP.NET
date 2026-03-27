using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.DTOs.Tests;

public class FeedbackDtoTests
{
    [Fact]
    public void FeedbackDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var feedbackDate = new DateTime(2024, 8, 20, 15, 30, 0);

        // Act
        var dto = new FeedbackDto(1, 10, 5, 3, 5, "Excellent service!", feedbackDate);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(10, dto.AppointmentId);
        Assert.Equal(5, dto.PatientId);
        Assert.Equal(3, dto.DoctorId);
        Assert.Equal(5, dto.Rating);
        Assert.Equal("Excellent service!", dto.Comments);
        Assert.Equal(feedbackDate, dto.FeedbackDate);
    }

    [Fact]
    public void FeedbackDto_WithNullComments_ShouldAcceptNull()
    {
        // Arrange
        var feedbackDate = DateTime.UtcNow;

        // Act
        var dto = new FeedbackDto(1, 10, 5, 3, 4, null, feedbackDate);

        // Assert
        Assert.Null(dto.Comments);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void FeedbackDto_WithDifferentRatings_ShouldInitializeCorrectly(int rating)
    {
        // Arrange
        var feedbackDate = DateTime.UtcNow;

        // Act
        var dto = new FeedbackDto(1, 10, 5, 3, rating, "Comment", feedbackDate);

        // Assert
        Assert.Equal(rating, dto.Rating);
    }

    [Fact]
    public void FeedbackDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var feedbackDate = new DateTime(2024, 8, 20);
        var dto1 = new FeedbackDto(1, 10, 5, 3, 5, "Great!", feedbackDate);
        var dto2 = new FeedbackDto(1, 10, 5, 3, 5, "Great!", feedbackDate);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void FeedbackDto_WithDifferentRatings_ShouldNotBeEqual()
    {
        // Arrange
        var feedbackDate = new DateTime(2024, 8, 20);
        var dto1 = new FeedbackDto(1, 10, 5, 3, 5, "Great!", feedbackDate);
        var dto2 = new FeedbackDto(1, 10, 5, 3, 4, "Great!", feedbackDate);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void FeedbackDto_WithEmptyComments_ShouldAcceptEmptyString()
    {
        // Arrange
        var feedbackDate = DateTime.UtcNow;

        // Act
        var dto = new FeedbackDto(1, 10, 5, 3, 4, string.Empty, feedbackDate);

        // Assert
        Assert.Equal(string.Empty, dto.Comments);
    }
}
