using Xunit;
using ClinicManagement.Domain.Entities;
using System;

namespace ClinicManagement.Domain.Tests
{
    public class BillTests
    {
        [Fact]
        public void Bill_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedId = 1;

            // Act
            bill.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, bill.Id);
        }

        [Fact]
        public void Bill_PatientId_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedPatientId = 10;

            // Act
            bill.PatientId = expectedPatientId;

            // Assert
            Assert.Equal(expectedPatientId, bill.PatientId);
        }

        [Fact]
        public void Bill_AppointmentId_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedAppointmentId = 5;

            // Act
            bill.AppointmentId = expectedAppointmentId;

            // Assert
            Assert.Equal(expectedAppointmentId, bill.AppointmentId);
        }

        [Fact]
        public void Bill_Amount_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedAmount = 150.50m;

            // Act
            bill.Amount = expectedAmount;

            // Assert
            Assert.Equal(expectedAmount, bill.Amount);
        }

        [Fact]
        public void Bill_Amount_ShouldHandleZero()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Amount = 0m;

            // Assert
            Assert.Equal(0m, bill.Amount);
        }

        [Fact]
        public void Bill_Amount_ShouldHandleLargeValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedAmount = 99999.99m;

            // Act
            bill.Amount = expectedAmount;

            // Assert
            Assert.Equal(expectedAmount, bill.Amount);
        }

        [Fact]
        public void Bill_BillDate_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedDate = DateTime.UtcNow;

            // Act
            bill.BillDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, bill.BillDate);
        }

        [Fact]
        public void Bill_PaymentStatus_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedStatus = "Paid";

            // Act
            bill.PaymentStatus = expectedStatus;

            // Assert
            Assert.Equal(expectedStatus, bill.PaymentStatus);
        }

        [Fact]
        public void Bill_Description_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedDescription = "Consultation and medication";

            // Act
            bill.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, bill.Description);
        }

        [Fact]
        public void Bill_Description_CanBeNull()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Description = null;

            // Assert
            Assert.Null(bill.Description);
        }

        [Fact]
        public void Bill_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedDate = DateTime.UtcNow;

            // Act
            bill.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, bill.CreatedDate);
        }

        [Fact]
        public void Bill_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();
            var expectedDate = DateTime.UtcNow;

            // Act
            bill.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, bill.ModifiedDate);
        }

        [Fact]
        public void Bill_ModifiedDate_CanBeNull()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.ModifiedDate = null;

            // Assert
            Assert.Null(bill.ModifiedDate);
        }

        [Fact]
        public void Bill_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.IsActive = true;

            // Assert
            Assert.True(bill.IsActive);
        }

        [Fact]
        public void Bill_AllProperties_ShouldBeSettable()
        {
            // Arrange & Act
            var bill = new Bill
            {
                Id = 1,
                PatientId = 10,
                AppointmentId = 5,
                Amount = 250.75m,
                BillDate = DateTime.UtcNow,
                PaymentStatus = "Pending",
                Description = "Follow-up consultation",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true
            };

            // Assert
            Assert.Equal(1, bill.Id);
            Assert.Equal(10, bill.PatientId);
            Assert.Equal(5, bill.AppointmentId);
            Assert.Equal(250.75m, bill.Amount);
            Assert.Equal("Pending", bill.PaymentStatus);
            Assert.Equal("Follow-up consultation", bill.Description);
            Assert.True(bill.IsActive);
        }

        [Fact]
        public void Bill_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var bill = new Bill();

            // Assert
            Assert.Equal(string.Empty, bill.PaymentStatus);
            Assert.False(bill.IsActive);
            Assert.Equal(0m, bill.Amount);
        }

        [Fact]
        public void Bill_PaymentStatus_MultipleValues_ShouldBeSettable()
        {
            // Arrange
            var bill = new Bill();

            // Act & Assert - Paid
            bill.PaymentStatus = "Paid";
            Assert.Equal("Paid", bill.PaymentStatus);

            // Act & Assert - Pending
            bill.PaymentStatus = "Pending";
            Assert.Equal("Pending", bill.PaymentStatus);

            // Act & Assert - Overdue
            bill.PaymentStatus = "Overdue";
            Assert.Equal("Overdue", bill.PaymentStatus);

            // Act & Assert - Cancelled
            bill.PaymentStatus = "Cancelled";
            Assert.Equal("Cancelled", bill.PaymentStatus);
        }

        [Fact]
        public void Bill_Amount_ShouldHandleNegativeValue()
        {
            // Arrange
            var bill = new Bill();

            // Act
            bill.Amount = -100m;

            // Assert
            Assert.Equal(-100m, bill.Amount);
        }
    }
}
