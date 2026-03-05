using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class FeedbackDtoTests
{
    [Fact]
    public void FeedbackDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var feedbackDto = new FeedbackDto();

        // Assert
        Assert.Equal(0, feedbackDto.Id);
        Assert.Equal(0, feedbackDto.PatientId);
        Assert.Equal(string.Empty, feedbackDto.Message);
        Assert.Null(feedbackDto.Rating);
        Assert.Equal(DateTime.MinValue, feedbackDto.FeedbackDate);
        Assert.Null(feedbackDto.PatientName);
    }

    [Fact]
    public void FeedbackDto_ShouldSet_AllProperties()
    {
        // Arrange
        var feedbackDate = DateTime.UtcNow;

        // Act
        var feedbackDto = new FeedbackDto
        {
            Id = 1,
            PatientId = 10,
            Message = "Great service",
            Rating = 5,
            FeedbackDate = feedbackDate,
            PatientName = "Jane Doe"
        };

        // Assert
        Assert.Equal(1, feedbackDto.Id);
        Assert.Equal(10, feedbackDto.PatientId);
        Assert.Equal("Great service", feedbackDto.Message);
        Assert.Equal(5, feedbackDto.Rating);
        Assert.Equal(feedbackDate, feedbackDto.FeedbackDate);
        Assert.Equal("Jane Doe", feedbackDto.PatientName);
    }

    [Fact]
    public void FeedbackCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var feedbackCreateDto = new FeedbackCreateDto();

        // Assert
        Assert.Equal(0, feedbackCreateDto.PatientId);
        Assert.Equal(string.Empty, feedbackCreateDto.Message);
        Assert.Null(feedbackCreateDto.Rating);
    }

    [Fact]
    public void FeedbackCreateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var feedbackCreateDto = new FeedbackCreateDto
        {
            PatientId = 5,
            Message = "Excellent doctor",
            Rating = 4
        };

        // Assert
        Assert.Equal(5, feedbackCreateDto.PatientId);
        Assert.Equal("Excellent doctor", feedbackCreateDto.Message);
        Assert.Equal(4, feedbackCreateDto.Rating);
    }

    [Fact]
    public void FeedbackUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var feedbackUpdateDto = new FeedbackUpdateDto();

        // Assert
        Assert.Equal(string.Empty, feedbackUpdateDto.Message);
        Assert.Null(feedbackUpdateDto.Rating);
    }

    [Fact]
    public void FeedbackUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var feedbackUpdateDto = new FeedbackUpdateDto
        {
            Message = "Updated feedback",
            Rating = 3
        };

        // Assert
        Assert.Equal("Updated feedback", feedbackUpdateDto.Message);
        Assert.Equal(3, feedbackUpdateDto.Rating);
    }

    [Fact]
    public void FeedbackDto_Rating_ShouldAcceptNull()
    {
        // Arrange & Act
        var feedbackDto = new FeedbackDto { Rating = null };

        // Assert
        Assert.Null(feedbackDto.Rating);
    }

    [Fact]
    public void FeedbackDto_PatientName_ShouldAcceptNull()
    {
        // Arrange & Act
        var feedbackDto = new FeedbackDto { PatientName = null };

        // Assert
        Assert.Null(feedbackDto.PatientName);
    }

    [Fact]
    public void FeedbackDto_Rating_ShouldAcceptValidRange()
    {
        // Arrange & Act
        var feedbackDto1 = new FeedbackDto { Rating = 1 };
        var feedbackDto2 = new FeedbackDto { Rating = 5 };

        // Assert
        Assert.Equal(1, feedbackDto1.Rating);
        Assert.Equal(5, feedbackDto2.Rating);
    }
}
