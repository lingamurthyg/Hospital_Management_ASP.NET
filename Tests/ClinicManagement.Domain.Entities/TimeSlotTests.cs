using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests;

public class TimeSlotTests
{
    [Fact]
    public void TimeSlot_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var timeSlot = new TimeSlot();

        // Assert
        Assert.NotNull(timeSlot);
        Assert.True(timeSlot.IsAvailable);
        Assert.NotNull(timeSlot.Appointments);
        Assert.Empty(timeSlot.Appointments);
    }

    [Fact]
    public void TimeSlot_SetDoctorId_ShouldUpdateValue()
    {
        // Arrange
        var timeSlot = new TimeSlot();

        // Act
        timeSlot.DoctorId = 5;

        // Assert
        Assert.Equal(5, timeSlot.DoctorId);
    }

    [Fact]
    public void TimeSlot_SetStartTime_ShouldUpdateValue()
    {
        // Arrange
        var timeSlot = new TimeSlot();
        var startTime = new TimeSpan(9, 0, 0);

        // Act
        timeSlot.StartTime = startTime;

        // Assert
        Assert.Equal(startTime, timeSlot.StartTime);
    }

    [Fact]
    public void TimeSlot_SetEndTime_ShouldUpdateValue()
    {
        // Arrange
        var timeSlot = new TimeSlot();
        var endTime = new TimeSpan(10, 0, 0);

        // Act
        timeSlot.EndTime = endTime;

        // Assert
        Assert.Equal(endTime, timeSlot.EndTime);
    }

    [Fact]
    public void TimeSlot_IsAvailable_DefaultShouldBeTrue()
    {
        // Arrange & Act
        var timeSlot = new TimeSlot();

        // Assert
        Assert.True(timeSlot.IsAvailable);
    }

    [Fact]
    public void TimeSlot_SetIsAvailable_ShouldUpdateValue()
    {
        // Arrange
        var timeSlot = new TimeSlot();

        // Act
        timeSlot.IsAvailable = false;

        // Assert
        Assert.False(timeSlot.IsAvailable);
    }

    [Fact]
    public void TimeSlot_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var timeSlot = new TimeSlot();
        var startTime = new TimeSpan(14, 30, 0);
        var endTime = new TimeSpan(15, 30, 0);

        // Act
        timeSlot.DoctorId = 10;
        timeSlot.StartTime = startTime;
        timeSlot.EndTime = endTime;
        timeSlot.IsAvailable = false;

        // Assert
        Assert.Equal(10, timeSlot.DoctorId);
        Assert.Equal(startTime, timeSlot.StartTime);
        Assert.Equal(endTime, timeSlot.EndTime);
        Assert.False(timeSlot.IsAvailable);
    }

    [Fact]
    public void TimeSlot_Doctor_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var timeSlot = new TimeSlot();

        // Act
        timeSlot.Doctor = null;

        // Assert
        Assert.Null(timeSlot.Doctor);
    }

    [Fact]
    public void TimeSlot_Appointments_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var timeSlot = new TimeSlot();

        // Assert
        Assert.NotNull(timeSlot.Appointments);
        Assert.IsAssignableFrom<ICollection<Appointment>>(timeSlot.Appointments);
        Assert.Empty(timeSlot.Appointments);
    }

    [Fact]
    public void TimeSlot_Appointments_ShouldAllowAddingAppointments()
    {
        // Arrange
        var timeSlot = new TimeSlot();
        var appointment = new Appointment { Id = 1 };

        // Act
        timeSlot.Appointments.Add(appointment);

        // Assert
        Assert.Single(timeSlot.Appointments);
        Assert.Contains(appointment, timeSlot.Appointments);
    }

    [Theory]
    [InlineData(8, 0, 0)]
    [InlineData(12, 30, 0)]
    [InlineData(18, 45, 30)]
    public void TimeSlot_StartTime_ShouldAcceptVariousTimeSpans(int hours, int minutes, int seconds)
    {
        // Arrange
        var timeSlot = new TimeSlot();
        var time = new TimeSpan(hours, minutes, seconds);

        // Act
        timeSlot.StartTime = time;

        // Assert
        Assert.Equal(time, timeSlot.StartTime);
    }
}
