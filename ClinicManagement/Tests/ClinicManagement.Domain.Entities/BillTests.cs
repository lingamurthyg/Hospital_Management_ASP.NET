using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class BillTests
{
    [Fact]
    public void Bill_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var bill = new Bill();

        // Assert
        Assert.Equal(0, bill.Id);
        Assert.Equal(0, bill.PatientId);
        Assert.Equal(0, bill.AppointmentId);
        Assert.Equal(0m, bill.Amount);
        Assert.Equal(DateTime.MinValue, bill.BillDate);
        Assert.Equal(string.Empty, bill.Status);
        Assert.Null(bill.Description);
        Assert.False(bill.IsActive);
        Assert.Equal(string.Empty, bill.CreatedBy);
        Assert.Null(bill.ModifiedBy);
        Assert.Null(bill.Patient);
    }

    [Fact]
    public void Bill_ShouldSet_IdProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Id = 1;

        // Assert
        Assert.Equal(1, bill.Id);
    }

    [Fact]
    public void Bill_ShouldSet_PatientIdProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.PatientId = 10;

        // Assert
        Assert.Equal(10, bill.PatientId);
    }

    [Fact]
    public void Bill_ShouldSet_AppointmentIdProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.AppointmentId = 5;

        // Assert
        Assert.Equal(5, bill.AppointmentId);
    }

    [Fact]
    public void Bill_ShouldSet_AmountProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Amount = 250.75m;

        // Assert
        Assert.Equal(250.75m, bill.Amount);
    }

    [Fact]
    public void Bill_ShouldSet_BillDateProperty()
    {
        // Arrange
        var bill = new Bill();
        var date = DateTime.UtcNow;

        // Act
        bill.BillDate = date;

        // Assert
        Assert.Equal(date, bill.BillDate);
    }

    [Fact]
    public void Bill_ShouldSet_StatusProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Status = "Paid";

        // Assert
        Assert.Equal("Paid", bill.Status);
    }

    [Fact]
    public void Bill_ShouldSet_DescriptionProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Description = "Consultation fee";

        // Assert
        Assert.Equal("Consultation fee", bill.Description);
    }

    [Fact]
    public void Bill_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.IsActive = true;

        // Assert
        Assert.True(bill.IsActive);
    }

    [Fact]
    public void Bill_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var bill = new Bill();
        var date = DateTime.UtcNow;

        // Act
        bill.CreatedDate = date;

        // Assert
        Assert.Equal(date, bill.CreatedDate);
    }

    [Fact]
    public void Bill_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var bill = new Bill();
        var date = DateTime.UtcNow;

        // Act
        bill.ModifiedDate = date;

        // Assert
        Assert.Equal(date, bill.ModifiedDate);
    }

    [Fact]
    public void Bill_ShouldSet_PatientProperty()
    {
        // Arrange
        var bill = new Bill();
        var patient = new Patient { Id = 1, UserId = 5 };

        // Act
        bill.Patient = patient;

        // Assert
        Assert.NotNull(bill.Patient);
        Assert.Equal(1, bill.Patient.Id);
    }

    [Fact]
    public void Bill_Description_ShouldAcceptNull()
    {
        // Arrange & Act
        var bill = new Bill { Description = null };

        // Assert
        Assert.Null(bill.Description);
    }

    [Fact]
    public void Bill_Amount_ShouldAcceptZero()
    {
        // Arrange & Act
        var bill = new Bill { Amount = 0m };

        // Assert
        Assert.Equal(0m, bill.Amount);
    }

    [Fact]
    public void Bill_Amount_ShouldAcceptNegativeValue()
    {
        // Arrange & Act
        var bill = new Bill { Amount = -50m };

        // Assert
        Assert.Equal(-50m, bill.Amount);
    }

    [Fact]
    public void Bill_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var patient = new Patient { Id = 1 };

        // Act
        var bill = new Bill
        {
            Id = 20,
            PatientId = 1,
            AppointmentId = 10,
            Amount = 500.00m,
            BillDate = date,
            Status = "Unpaid",
            Description = "Lab tests",
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Admin",
            Patient = patient
        };

        // Assert
        Assert.Equal(20, bill.Id);
        Assert.Equal(1, bill.PatientId);
        Assert.Equal(10, bill.AppointmentId);
        Assert.Equal(500.00m, bill.Amount);
        Assert.Equal(date, bill.BillDate);
        Assert.Equal("Unpaid", bill.Status);
        Assert.Equal("Lab tests", bill.Description);
        Assert.Equal(date, bill.CreatedDate);
        Assert.Equal(date, bill.ModifiedDate);
        Assert.True(bill.IsActive);
        Assert.Equal("System", bill.CreatedBy);
        Assert.Equal("Admin", bill.ModifiedBy);
        Assert.NotNull(bill.Patient);
    }
}
