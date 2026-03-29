using Xunit;
using System;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class BillTests
    {
        [Fact]
        public void Constructor_ShouldInitializeBillWithDefaultValues()
        {
            // Arrange & Act
            var bill = new Bill();

            // Assert
            Assert.Equal(0, bill.BillID);
            Assert.Equal(0, bill.PatientID);
            Assert.Equal(0, bill.AppointmentID);
            Assert.Equal(0m, bill.Amount);
            Assert.Equal(default(DateTime), bill.BillDate);
            Assert.Equal("Pending", bill.Status);
            Assert.Null(bill.Description);
            Assert.Equal(default(DateTime), bill.CreatedDate);
            Assert.Null(bill.ModifiedDate);
            Assert.False(bill.IsActive);
            Assert.Null(bill.Patient);
            Assert.Null(bill.Appointment);
        }

        [Fact]
        public void BillID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            int expectedId = 500;

            // Act
            bill.BillID = expectedId;

            // Assert
            Assert.Equal(expectedId, bill.BillID);
        }

        [Fact]
        public void PatientID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            int expectedPatientId = 123;

            // Act
            bill.PatientID = expectedPatientId;

            // Assert
            Assert.Equal(expectedPatientId, bill.PatientID);
        }

        [Fact]
        public void AppointmentID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            int expectedAppointmentId = 456;

            // Act
            bill.AppointmentID = expectedAppointmentId;

            // Assert
            Assert.Equal(expectedAppointmentId, bill.AppointmentID);
        }

        [Fact]
        public void Amount_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            decimal expectedAmount = 150.50m;

            // Act
            bill.Amount = expectedAmount;

            // Assert
            Assert.Equal(expectedAmount, bill.Amount);
        }

        [Fact]
        public void Amount_ShouldAcceptZeroValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Amount = 0m;

            // Assert
            Assert.Equal(0m, bill.Amount);
        }

        [Fact]
        public void Amount_ShouldAcceptLargeValue()
        {
            // Arrange
            var bill = new Bill();
            decimal largeAmount = 10000.99m;

            // Act
            bill.Amount = largeAmount;

            // Assert
            Assert.Equal(largeAmount, bill.Amount);
        }

        [Fact]
        public void Amount_ShouldAcceptDecimalPrecision()
        {
            // Arrange
            var bill = new Bill();
            decimal preciseAmount = 99.99m;

            // Act
            bill.Amount = preciseAmount;

            // Assert
            Assert.Equal(preciseAmount, bill.Amount);
        }

        [Fact]
        public void BillDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            DateTime expectedBillDate = new DateTime(2024, 9, 1);

            // Act
            bill.BillDate = expectedBillDate;

            // Assert
            Assert.Equal(expectedBillDate, bill.BillDate);
        }

        [Fact]
        public void Status_ShouldDefaultToPending()
        {
            // Arrange & Act
            var bill = new Bill();

            // Assert
            Assert.Equal("Pending", bill.Status);
        }

        [Fact]
        public void Status_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            string expectedStatus = "Paid";

            // Act
            bill.Status = expectedStatus;

            // Assert
            Assert.Equal(expectedStatus, bill.Status);
        }

        [Fact]
        public void Status_ShouldAcceptDifferentStatusValues()
        {
            // Arrange
            var bill = new Bill();
            string[] statuses = { "Pending", "Paid", "Cancelled", "Overdue" };

            foreach (var status in statuses)
            {
                // Act
                bill.Status = status;

                // Assert
                Assert.Equal(status, bill.Status);
            }
        }

        [Fact]
        public void Description_ShouldAcceptNullValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Description = null;

            // Assert
            Assert.Null(bill.Description);
        }

        [Fact]
        public void Description_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            string expectedDescription = "Consultation fee";

            // Act
            bill.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, bill.Description);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            DateTime expectedCreatedDate = new DateTime(2024, 8, 1);

            // Act
            bill.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, bill.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.ModifiedDate = null;

            // Assert
            Assert.Null(bill.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            bill.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, bill.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.IsActive = true;

            // Assert
            Assert.True(bill.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.IsActive = false;

            // Assert
            Assert.False(bill.IsActive);
        }

        [Fact]
        public void Patient_ShouldAcceptNullValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Patient = null;

            // Assert
            Assert.Null(bill.Patient);
        }

        [Fact]
        public void Patient_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            var patient = new Patient { PatientID = 1, Name = "Jane Doe" };

            // Act
            bill.Patient = patient;

            // Assert
            Assert.NotNull(bill.Patient);
            Assert.Equal(patient, bill.Patient);
        }

        [Fact]
        public void Appointment_ShouldAcceptNullValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Appointment = null;

            // Assert
            Assert.Null(bill.Appointment);
        }

        [Fact]
        public void Appointment_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var bill = new Bill();
            var appointment = new Appointment { AppointmentID = 1 };

            // Act
            bill.Appointment = appointment;

            // Assert
            Assert.NotNull(bill.Appointment);
            Assert.Equal(appointment, bill.Appointment);
        }

        [Fact]
        public void BillProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedBillDate = new DateTime(2024, 10, 1);
            var expectedCreatedDate = new DateTime(2024, 9, 15);
            var expectedModifiedDate = new DateTime(2024, 9, 20);

            // Act
            var bill = new Bill
            {
                BillID = 777,
                PatientID = 888,
                AppointmentID = 999,
                Amount = 250.75m,
                BillDate = expectedBillDate,
                Status = "Paid",
                Description = "General consultation and lab tests",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(777, bill.BillID);
            Assert.Equal(888, bill.PatientID);
            Assert.Equal(999, bill.AppointmentID);
            Assert.Equal(250.75m, bill.Amount);
            Assert.Equal(expectedBillDate, bill.BillDate);
            Assert.Equal("Paid", bill.Status);
            Assert.Equal("General consultation and lab tests", bill.Description);
            Assert.Equal(expectedCreatedDate, bill.CreatedDate);
            Assert.Equal(expectedModifiedDate, bill.ModifiedDate);
            Assert.True(bill.IsActive);
        }

        [Fact]
        public void BillID_ShouldAcceptZeroValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.BillID = 0;

            // Assert
            Assert.Equal(0, bill.BillID);
        }

        [Fact]
        public void Amount_ShouldAcceptSmallDecimalValue()
        {
            // Arrange
            var bill = new Bill();
            decimal smallAmount = 0.01m;

            // Act
            bill.Amount = smallAmount;

            // Assert
            Assert.Equal(smallAmount, bill.Amount);
        }

        [Fact]
        public void Description_ShouldAcceptLongText()
        {
            // Arrange
            var bill = new Bill();
            string longDescription = new string('A', 500);

            // Act
            bill.Description = longDescription;

            // Assert
            Assert.Equal(longDescription, bill.Description);
        }

        [Fact]
        public void BillDate_ShouldAcceptFutureDate()
        {
            // Arrange
            var bill = new Bill();
            DateTime futureDate = DateTime.Now.AddDays(30);

            // Act
            bill.BillDate = futureDate;

            // Assert
            Assert.Equal(futureDate, bill.BillDate);
        }

        [Fact]
        public void BillDate_ShouldAcceptPastDate()
        {
            // Arrange
            var bill = new Bill();
            DateTime pastDate = DateTime.Now.AddDays(-90);

            // Act
            bill.BillDate = pastDate;

            // Assert
            Assert.Equal(pastDate, bill.BillDate);
        }
    }
}
