using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests;

public class BillTests
{
    [Fact]
    public void Bill_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var bill = new Bill();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.NotNull(bill);
        Assert.False(bill.IsPaid);
        Assert.InRange(bill.BillDate, beforeCreation, afterCreation);
    }

    [Fact]
    public void Bill_BillDate_DefaultShouldBeUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var bill = new Bill();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.InRange(bill.BillDate, beforeCreation, afterCreation);
    }

    [Fact]
    public void Bill_IsPaid_DefaultShouldBeFalse()
    {
        // Arrange & Act
        var bill = new Bill();

        // Assert
        Assert.False(bill.IsPaid);
    }

    [Fact]
    public void Bill_SetAppointmentId_ShouldUpdateValue()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.AppointmentId = 75;

        // Assert
        Assert.Equal(75, bill.AppointmentId);
    }

    [Fact]
    public void Bill_SetPatientId_ShouldUpdateValue()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.PatientId = 150;

        // Assert
        Assert.Equal(150, bill.PatientId);
    }

    [Fact]
    public void Bill_SetAmount_ShouldUpdateValue()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Amount = 250.50m;

        // Assert
        Assert.Equal(250.50m, bill.Amount);
    }

    [Fact]
    public void Bill_SetIsPaid_ShouldUpdateValue()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.IsPaid = true;

        // Assert
        Assert.True(bill.IsPaid);
    }

    [Fact]
    public void Bill_SetBillDate_ShouldUpdateValue()
    {
        // Arrange
        var bill = new Bill();
        var customDate = new DateTime(2024, 9, 12, 16, 45, 0);

        // Act
        bill.BillDate = customDate;

        // Assert
        Assert.Equal(customDate, bill.BillDate);
    }

    [Fact]
    public void Bill_NavigationProperties_ShouldAcceptNull()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Appointment = null;
        bill.Patient = null;

        // Assert
        Assert.Null(bill.Appointment);
        Assert.Null(bill.Patient);
    }

    [Fact]
    public void Bill_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var bill = new Bill();
        var date = new DateTime(2024, 7, 20, 11, 0, 0);

        // Act
        bill.AppointmentId = 5;
        bill.PatientId = 10;
        bill.Amount = 500.00m;
        bill.IsPaid = true;
        bill.BillDate = date;

        // Assert
        Assert.Equal(5, bill.AppointmentId);
        Assert.Equal(10, bill.PatientId);
        Assert.Equal(500.00m, bill.Amount);
        Assert.True(bill.IsPaid);
        Assert.Equal(date, bill.BillDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100.50)]
    [InlineData(9999.99)]
    public void Bill_Amount_ShouldAcceptVariousDecimalValues(decimal amount)
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Amount = amount;

        // Assert
        Assert.Equal(amount, bill.Amount);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Bill_IsPaid_ShouldAcceptBooleanValues(bool isPaid)
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.IsPaid = isPaid;

        // Assert
        Assert.Equal(isPaid, bill.IsPaid);
    }

    [Fact]
    public void Bill_Amount_ShouldAcceptZero()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Amount = 0m;

        // Assert
        Assert.Equal(0m, bill.Amount);
    }
}
