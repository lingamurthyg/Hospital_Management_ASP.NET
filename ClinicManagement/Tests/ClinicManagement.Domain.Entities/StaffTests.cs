using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class StaffTests
{
    [Fact]
    public void Staff_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var staff = new Staff();

        // Assert
        Assert.Equal(0, staff.Id);
        Assert.Equal(0, staff.UserId);
        Assert.Equal(string.Empty, staff.Position);
        Assert.Null(staff.Department);
        Assert.False(staff.IsActive);
        Assert.Equal(string.Empty, staff.CreatedBy);
        Assert.Null(staff.ModifiedBy);
        Assert.Null(staff.User);
    }

    [Fact]
    public void Staff_ShouldSet_IdProperty()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Id = 1;

        // Assert
        Assert.Equal(1, staff.Id);
    }

    [Fact]
    public void Staff_ShouldSet_UserIdProperty()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.UserId = 5;

        // Assert
        Assert.Equal(5, staff.UserId);
    }

    [Fact]
    public void Staff_ShouldSet_PositionProperty()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Position = "Nurse";

        // Assert
        Assert.Equal("Nurse", staff.Position);
    }

    [Fact]
    public void Staff_ShouldSet_DepartmentProperty()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Department = "Emergency";

        // Assert
        Assert.Equal("Emergency", staff.Department);
    }

    [Fact]
    public void Staff_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.IsActive = true;

        // Assert
        Assert.True(staff.IsActive);
    }

    [Fact]
    public void Staff_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var staff = new Staff();
        var date = DateTime.UtcNow;

        // Act
        staff.CreatedDate = date;

        // Assert
        Assert.Equal(date, staff.CreatedDate);
    }

    [Fact]
    public void Staff_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var staff = new Staff();
        var date = DateTime.UtcNow;

        // Act
        staff.ModifiedDate = date;

        // Assert
        Assert.Equal(date, staff.ModifiedDate);
    }

    [Fact]
    public void Staff_ShouldSet_UserProperty()
    {
        // Arrange
        var staff = new Staff();
        var user = new User { Id = 1, Name = "Staff Member" };

        // Act
        staff.User = user;

        // Assert
        Assert.NotNull(staff.User);
        Assert.Equal(1, staff.User.Id);
        Assert.Equal("Staff Member", staff.User.Name);
    }

    [Fact]
    public void Staff_Department_ShouldAcceptNull()
    {
        // Arrange & Act
        var staff = new Staff { Department = null };

        // Assert
        Assert.Null(staff.Department);
    }

    [Fact]
    public void Staff_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var user = new User { Id = 1 };

        // Act
        var staff = new Staff
        {
            Id = 7,
            UserId = 1,
            Position = "Receptionist",
            Department = "Front Desk",
            CreatedDate = date,
            ModifiedDate = date,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Admin",
            User = user
        };

        // Assert
        Assert.Equal(7, staff.Id);
        Assert.Equal(1, staff.UserId);
        Assert.Equal("Receptionist", staff.Position);
        Assert.Equal("Front Desk", staff.Department);
        Assert.Equal(date, staff.CreatedDate);
        Assert.Equal(date, staff.ModifiedDate);
        Assert.True(staff.IsActive);
        Assert.Equal("System", staff.CreatedBy);
        Assert.Equal("Admin", staff.ModifiedBy);
        Assert.NotNull(staff.User);
    }

    [Fact]
    public void Staff_Position_ShouldAcceptEmptyString()
    {
        // Arrange & Act
        var staff = new Staff { Position = string.Empty };

        // Assert
        Assert.Equal(string.Empty, staff.Position);
    }
}
