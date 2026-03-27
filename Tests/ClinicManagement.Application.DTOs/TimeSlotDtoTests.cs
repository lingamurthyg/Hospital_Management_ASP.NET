using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.DTOs.Tests;

public class TimeSlotDtoTests
{
    [Fact]
    public void TimeSlotDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(10, 0, 0);

        // Act
        var dto = new TimeSlotDto(1, 5, startTime, endTime, true);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(5, dto.DoctorId);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.True(dto.IsAvailable);
    }

    [Fact]
    public void TimeSlotDto_WithUnavailableSlot_ShouldSetIsAvailableFalse()
    {
        // Arrange
        var startTime = new TimeSpan(14, 0, 0);
        var endTime = new TimeSpan(15, 0, 0);

        // Act
        var dto = new TimeSlotDto(2, 10, startTime, endTime, false);

        // Assert
        Assert.False(dto.IsAvailable);
    }

    [Theory]
    [InlineData(1, 5, 8, 0, 9, 0, true)]
    [InlineData(2, 10, 10, 30, 11, 30, false)]
    [InlineData(3, 15, 16, 0, 17, 0, true)]
    public void TimeSlotDto_WithVariousValues_ShouldInitializeCorrectly(int id, int doctorId, int startHour, int startMin, int endHour, int endMin, bool isAvailable)
    {
        // Arrange
        var startTime = new TimeSpan(startHour, startMin, 0);
        var endTime = new TimeSpan(endHour, endMin, 0);

        // Act
        var dto = new TimeSlotDto(id, doctorId, startTime, endTime, isAvailable);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(doctorId, dto.DoctorId);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.Equal(isAvailable, dto.IsAvailable);
    }

    [Fact]
    public void TimeSlotDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(10, 0, 0);
        var dto1 = new TimeSlotDto(1, 5, startTime, endTime, true);
        var dto2 = new TimeSlotDto(1, 5, startTime, endTime, true);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void TimeSlotDto_DifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var startTime1 = new TimeSpan(9, 0, 0);
        var endTime1 = new TimeSpan(10, 0, 0);
        var startTime2 = new TimeSpan(11, 0, 0);
        var endTime2 = new TimeSpan(12, 0, 0);
        var dto1 = new TimeSlotDto(1, 5, startTime1, endTime1, true);
        var dto2 = new TimeSlotDto(2, 5, startTime2, endTime2, true);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }
}
