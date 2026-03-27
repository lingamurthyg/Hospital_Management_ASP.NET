using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.DTOs.Tests;

public class BillDtoTests
{
    [Fact]
    public void BillDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var billDate = new DateTime(2024, 6, 15, 10, 30, 0);

        // Act
        var dto = new BillDto(1, 10, 5, "John Doe", 250.50m, true, billDate);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(10, dto.AppointmentId);
        Assert.Equal(5, dto.PatientId);
        Assert.Equal("John Doe", dto.PatientName);
        Assert.Equal(250.50m, dto.Amount);
        Assert.True(dto.IsPaid);
        Assert.Equal(billDate, dto.BillDate);
    }

    [Fact]
    public void BillDto_WithUnpaidStatus_ShouldSetIsPaidFalse()
    {
        // Arrange
        var billDate = new DateTime(2024, 3, 20);

        // Act
        var dto = new BillDto(2, 20, 10, "Jane Smith", 500m, false, billDate);

        // Assert
        Assert.False(dto.IsPaid);
    }

    [Theory]
    [InlineData(100.00)]
    [InlineData(250.75)]
    [InlineData(999.99)]
    public void BillDto_WithDifferentAmounts_ShouldInitializeCorrectly(decimal amount)
    {
        // Arrange
        var billDate = DateTime.UtcNow;

        // Act
        var dto = new BillDto(1, 1, 1, "Patient", amount, true, billDate);

        // Assert
        Assert.Equal(amount, dto.Amount);
    }

    [Fact]
    public void BillDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var billDate = new DateTime(2024, 6, 15);
        var dto1 = new BillDto(1, 10, 5, "John Doe", 250.50m, true, billDate);
        var dto2 = new BillDto(1, 10, 5, "John Doe", 250.50m, true, billDate);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void BillDto_WithDifferentAmounts_ShouldNotBeEqual()
    {
        // Arrange
        var billDate = new DateTime(2024, 6, 15);
        var dto1 = new BillDto(1, 10, 5, "John Doe", 250.50m, true, billDate);
        var dto2 = new BillDto(1, 10, 5, "John Doe", 300.00m, true, billDate);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void BillDto_WithDifferentPaymentStatuses_ShouldInitializeCorrectly(bool isPaid)
    {
        // Arrange
        var billDate = DateTime.UtcNow;

        // Act
        var dto = new BillDto(1, 1, 1, "Patient", 100m, isPaid, billDate);

        // Assert
        Assert.Equal(isPaid, dto.IsPaid);
    }
}
