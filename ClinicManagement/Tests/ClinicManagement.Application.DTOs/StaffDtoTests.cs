using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Tests.Application.DTOs;

public class StaffDtoTests
{
    [Fact]
    public void StaffDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var staffDto = new StaffDto();

        // Assert
        Assert.Equal(0, staffDto.Id);
        Assert.Equal(0, staffDto.UserId);
        Assert.Equal(string.Empty, staffDto.Position);
        Assert.Null(staffDto.Department);
        Assert.Null(staffDto.UserName);
    }

    [Fact]
    public void StaffDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var staffDto = new StaffDto
        {
            Id = 1,
            UserId = 10,
            Position = "Nurse",
            Department = "Emergency",
            UserName = "Jane Staff"
        };

        // Assert
        Assert.Equal(1, staffDto.Id);
        Assert.Equal(10, staffDto.UserId);
        Assert.Equal("Nurse", staffDto.Position);
        Assert.Equal("Emergency", staffDto.Department);
        Assert.Equal("Jane Staff", staffDto.UserName);
    }

    [Fact]
    public void StaffCreateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var staffCreateDto = new StaffCreateDto();

        // Assert
        Assert.Equal(0, staffCreateDto.UserId);
        Assert.Equal(string.Empty, staffCreateDto.Position);
        Assert.Null(staffCreateDto.Department);
    }

    [Fact]
    public void StaffCreateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var staffCreateDto = new StaffCreateDto
        {
            UserId = 5,
            Position = "Receptionist",
            Department = "Front Desk"
        };

        // Assert
        Assert.Equal(5, staffCreateDto.UserId);
        Assert.Equal("Receptionist", staffCreateDto.Position);
        Assert.Equal("Front Desk", staffCreateDto.Department);
    }

    [Fact]
    public void StaffUpdateDto_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var staffUpdateDto = new StaffUpdateDto();

        // Assert
        Assert.Equal(string.Empty, staffUpdateDto.Position);
        Assert.Null(staffUpdateDto.Department);
    }

    [Fact]
    public void StaffUpdateDto_ShouldSet_AllProperties()
    {
        // Arrange & Act
        var staffUpdateDto = new StaffUpdateDto
        {
            Position = "Senior Nurse",
            Department = "ICU"
        };

        // Assert
        Assert.Equal("Senior Nurse", staffUpdateDto.Position);
        Assert.Equal("ICU", staffUpdateDto.Department);
    }

    [Fact]
    public void StaffDto_Department_ShouldAcceptNull()
    {
        // Arrange & Act
        var staffDto = new StaffDto { Department = null };

        // Assert
        Assert.Null(staffDto.Department);
    }

    [Fact]
    public void StaffDto_UserName_ShouldAcceptNull()
    {
        // Arrange & Act
        var staffDto = new StaffDto { UserName = null };

        // Assert
        Assert.Null(staffDto.UserName);
    }
}
