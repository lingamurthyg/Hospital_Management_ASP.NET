using Xunit;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Enums.Tests;

public class AppointmentStatusTests
{
    [Fact]
    public void AppointmentStatus_PendingValue_ShouldBeZero()
    {
        // Arrange & Act
        var status = AppointmentStatus.Pending;

        // Assert
        Assert.Equal(0, (int)status);
    }

    [Fact]
    public void AppointmentStatus_ApprovedValue_ShouldBeOne()
    {
        // Arrange & Act
        var status = AppointmentStatus.Approved;

        // Assert
        Assert.Equal(1, (int)status);
    }

    [Fact]
    public void AppointmentStatus_CompletedValue_ShouldBeTwo()
    {
        // Arrange & Act
        var status = AppointmentStatus.Completed;

        // Assert
        Assert.Equal(2, (int)status);
    }

    [Fact]
    public void AppointmentStatus_CancelledValue_ShouldBeThree()
    {
        // Arrange & Act
        var status = AppointmentStatus.Cancelled;

        // Assert
        Assert.Equal(3, (int)status);
    }

    [Fact]
    public void AppointmentStatus_AllValues_ShouldBeDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues<AppointmentStatus>();

        // Assert
        Assert.Equal(4, values.Length);
        Assert.Contains(AppointmentStatus.Pending, values);
        Assert.Contains(AppointmentStatus.Approved, values);
        Assert.Contains(AppointmentStatus.Completed, values);
        Assert.Contains(AppointmentStatus.Cancelled, values);
    }

    [Theory]
    [InlineData(AppointmentStatus.Pending)]
    [InlineData(AppointmentStatus.Approved)]
    [InlineData(AppointmentStatus.Completed)]
    [InlineData(AppointmentStatus.Cancelled)]
    public void AppointmentStatus_ValidValue_ShouldBeDefined(AppointmentStatus status)
    {
        // Arrange & Act
        var isDefined = Enum.IsDefined(typeof(AppointmentStatus), status);

        // Assert
        Assert.True(isDefined);
    }
}
