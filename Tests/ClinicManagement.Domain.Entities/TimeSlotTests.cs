using Xunit;
using System;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class TimeSlotTests
    {
        [Fact]
        public void Constructor_ShouldInitializeTimeSlotWithDefaultValues()
        {
            // Arrange & Act
            var timeSlot = new TimeSlot();

            // Assert
            Assert.Equal(0, timeSlot.TimeSlotID);
            Assert.Equal(0, timeSlot.DoctorID);
            Assert.Equal(default(DateTime), timeSlot.SlotDate);
            Assert.Equal(default(TimeSpan), timeSlot.StartTime);
            Assert.Equal(default(TimeSpan), timeSlot.EndTime);
            Assert.False(timeSlot.IsAvailable);
            Assert.Equal(default(DateTime), timeSlot.CreatedDate);
            Assert.Null(timeSlot.ModifiedDate);
            Assert.False(timeSlot.IsActive);
            Assert.Null(timeSlot.Doctor);
        }

        [Fact]
        public void TimeSlotID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            int expectedId = 100;

            // Act
            timeSlot.TimeSlotID = expectedId;

            // Assert
            Assert.Equal(expectedId, timeSlot.TimeSlotID);
        }

        [Fact]
        public void DoctorID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            int expectedDoctorId = 5;

            // Act
            timeSlot.DoctorID = expectedDoctorId;

            // Assert
            Assert.Equal(expectedDoctorId, timeSlot.DoctorID);
        }

        [Fact]
        public void SlotDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            DateTime expectedDate = new DateTime(2024, 5, 15);

            // Act
            timeSlot.SlotDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, timeSlot.SlotDate);
        }

        [Fact]
        public void StartTime_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            TimeSpan expectedStartTime = new TimeSpan(9, 0, 0);

            // Act
            timeSlot.StartTime = expectedStartTime;

            // Assert
            Assert.Equal(expectedStartTime, timeSlot.StartTime);
        }

        [Fact]
        public void EndTime_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            TimeSpan expectedEndTime = new TimeSpan(10, 0, 0);

            // Act
            timeSlot.EndTime = expectedEndTime;

            // Assert
            Assert.Equal(expectedEndTime, timeSlot.EndTime);
        }

        [Fact]
        public void IsAvailable_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.IsAvailable = true;

            // Assert
            Assert.True(timeSlot.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.IsAvailable = false;

            // Assert
            Assert.False(timeSlot.IsAvailable);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            DateTime expectedCreatedDate = new DateTime(2024, 1, 1);

            // Act
            timeSlot.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, timeSlot.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.ModifiedDate = null;

            // Assert
            Assert.Null(timeSlot.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            timeSlot.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, timeSlot.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.IsActive = true;

            // Assert
            Assert.True(timeSlot.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.IsActive = false;

            // Assert
            Assert.False(timeSlot.IsActive);
        }

        [Fact]
        public void Doctor_ShouldAcceptNullValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.Doctor = null;

            // Assert
            Assert.Null(timeSlot.Doctor);
        }

        [Fact]
        public void Doctor_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            var doctor = new Doctor { DoctorID = 1, Name = "Dr. Smith" };

            // Act
            timeSlot.Doctor = doctor;

            // Assert
            Assert.NotNull(timeSlot.Doctor);
            Assert.Equal(doctor, timeSlot.Doctor);
        }

        [Fact]
        public void TimeSlotProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 6, 1);
            var expectedStartTime = new TimeSpan(14, 0, 0);
            var expectedEndTime = new TimeSpan(15, 0, 0);
            var expectedCreatedDate = new DateTime(2024, 5, 1);
            var expectedModifiedDate = new DateTime(2024, 5, 15);

            // Act
            var timeSlot = new TimeSlot
            {
                TimeSlotID = 50,
                DoctorID = 10,
                SlotDate = expectedDate,
                StartTime = expectedStartTime,
                EndTime = expectedEndTime,
                IsAvailable = true,
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(50, timeSlot.TimeSlotID);
            Assert.Equal(10, timeSlot.DoctorID);
            Assert.Equal(expectedDate, timeSlot.SlotDate);
            Assert.Equal(expectedStartTime, timeSlot.StartTime);
            Assert.Equal(expectedEndTime, timeSlot.EndTime);
            Assert.True(timeSlot.IsAvailable);
            Assert.Equal(expectedCreatedDate, timeSlot.CreatedDate);
            Assert.Equal(expectedModifiedDate, timeSlot.ModifiedDate);
            Assert.True(timeSlot.IsActive);
        }

        [Fact]
        public void StartTime_ShouldAcceptMidnightValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            TimeSpan midnight = new TimeSpan(0, 0, 0);

            // Act
            timeSlot.StartTime = midnight;

            // Assert
            Assert.Equal(midnight, timeSlot.StartTime);
        }

        [Fact]
        public void EndTime_ShouldAcceptEndOfDayValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            TimeSpan endOfDay = new TimeSpan(23, 59, 59);

            // Act
            timeSlot.EndTime = endOfDay;

            // Assert
            Assert.Equal(endOfDay, timeSlot.EndTime);
        }

        [Fact]
        public void TimeSlotID_ShouldAcceptZeroValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.TimeSlotID = 0;

            // Assert
            Assert.Equal(0, timeSlot.TimeSlotID);
        }

        [Fact]
        public void DoctorID_ShouldAcceptZeroValue()
        {
            // Arrange
            var timeSlot = new TimeSlot();

            // Act
            timeSlot.DoctorID = 0;

            // Assert
            Assert.Equal(0, timeSlot.DoctorID);
        }

        [Fact]
        public void StartTime_ShouldAcceptHalfHourIntervals()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            TimeSpan halfHour = new TimeSpan(9, 30, 0);

            // Act
            timeSlot.StartTime = halfHour;

            // Assert
            Assert.Equal(halfHour, timeSlot.StartTime);
        }

        [Fact]
        public void EndTime_ShouldBeGreaterThanStartTime()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(10, 0, 0);

            // Act
            timeSlot.StartTime = startTime;
            timeSlot.EndTime = endTime;

            // Assert
            Assert.True(timeSlot.EndTime > timeSlot.StartTime);
        }

        [Fact]
        public void SlotDate_ShouldAcceptFutureDate()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            DateTime futureDate = DateTime.Now.AddDays(30);

            // Act
            timeSlot.SlotDate = futureDate;

            // Assert
            Assert.Equal(futureDate, timeSlot.SlotDate);
        }

        [Fact]
        public void SlotDate_ShouldAcceptPastDate()
        {
            // Arrange
            var timeSlot = new TimeSlot();
            DateTime pastDate = DateTime.Now.AddDays(-30);

            // Act
            timeSlot.SlotDate = pastDate;

            // Assert
            Assert.Equal(pastDate, timeSlot.SlotDate);
        }
    }
}
