using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class BillDtoTests
{
    [Fact]
    public void BillDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var billDto = new BillDto();

        // Assert
        Assert.Equal(0, billDto.Id);
        Assert.Equal(0, billDto.PatientId);
        Assert.Equal(0, billDto.AppointmentId);
        Assert.Equal(0m, billDto.Amount);
        Assert.Equal(DateTime.MinValue, billDto.BillDate);
        Assert.Equal(string.Empty, billDto.Status);
        Assert.Null(billDto.Description);
        Assert.Null(billDto.PatientName);
    }

    [Fact]
    public void BillDto_ShouldSet_AllProperties()
    {
        // Arrange
        var billDate = DateTime.UtcNow;

        // Act
        var billDto = new BillDto
        {
            Id = 1,
            PatientId = 10,
            AppointmentId = 5,
            Amount = 250.00m,
            BillDate = billDate,
            Status = "Paid",
            Description = "Consultation",
            PatientName = "John Doe"
        };

        // Assert
        Assert.Equal(1, billDto.Id);
        Assert.Equal(10, billDto.PatientId);
        Assert.Equal(5, billDto.AppointmentId);
        Assert.Equal(250.00m, billDto.Amount);
        Assert.Equal(billDate, billDto.BillDate);
        Assert.Equal("Paid", billDto.Status);
        Assert.Equal("Consultation", billDto.Description);
        Assert.Equal("John Doe", billDto.PatientName);
    }

    [Fact]
    public void BillCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var billCreateDto = new BillCreateDto();

        // Assert
        Assert.Equal(0, billCreateDto.PatientId);
        Assert.Equal(0, billCreateDto.AppointmentId);
        Assert.Equal(0m, billCreateDto.Amount);
        Assert.Equal("Unpaid", billCreateDto.Status);
        Assert.Null(billCreateDto.Description);
    }

    [Fact]
    public void BillCreateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var billCreateDto = new BillCreateDto
        {
            PatientId = 5,
            AppointmentId = 10,
            Amount = 300.00m,
            Status = "Pending",
            Description = "Lab tests"
        };

        // Assert
        Assert.Equal(5, billCreateDto.PatientId);
        Assert.Equal(10, billCreateDto.AppointmentId);
        Assert.Equal(300.00m, billCreateDto.Amount);
        Assert.Equal("Pending", billCreateDto.Status);
        Assert.Equal("Lab tests", billCreateDto.Description);
    }

    [Fact]
    public void BillUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var billUpdateDto = new BillUpdateDto();

        // Assert
        Assert.Equal(0m, billUpdateDto.Amount);
        Assert.Equal(string.Empty, billUpdateDto.Status);
        Assert.Null(billUpdateDto.Description);
    }

    [Fact]
    public void BillUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var billUpdateDto = new BillUpdateDto
        {
            Amount = 150.00m,
            Status = "Paid",
            Description = "Updated description"
        };

        // Assert
        Assert.Equal(150.00m, billUpdateDto.Amount);
        Assert.Equal("Paid", billUpdateDto.Status);
        Assert.Equal("Updated description", billUpdateDto.Description);
    }

    [Fact]
    public void BillDto_Description_ShouldAcceptNull()
    {
        // Arrange & Act
        var billDto = new BillDto { Description = null };

        // Assert
        Assert.Null(billDto.Description);
    }

    [Fact]
    public void BillDto_PatientName_ShouldAcceptNull()
    {
        // Arrange & Act
        var billDto = new BillDto { PatientName = null };

        // Assert
        Assert.Null(billDto.PatientName);
    }

    [Fact]
    public void BillCreateDto_Status_DefaultsToUnpaid()
    {
        // Arrange & Act
        var billCreateDto = new BillCreateDto();

        // Assert
        Assert.Equal("Unpaid", billCreateDto.Status);
    }
}
