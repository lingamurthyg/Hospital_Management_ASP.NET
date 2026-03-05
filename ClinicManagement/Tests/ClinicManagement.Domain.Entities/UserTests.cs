using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void User_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        Assert.Equal(0, user.Id);
        Assert.Equal(string.Empty, user.Name);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.Password);
        Assert.Equal(string.Empty, user.PhoneNo);
        Assert.Null(user.Address);
        Assert.Equal(string.Empty, user.Gender);
        Assert.Equal(DateTime.MinValue, user.BirthDate);
        Assert.Equal(0, user.UserType);
        Assert.False(user.IsActive);
        Assert.Equal(string.Empty, user.CreatedBy);
        Assert.Null(user.ModifiedBy);
    }

    [Fact]
    public void User_ShouldSet_IdProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Id = 1;

        // Assert
        Assert.Equal(1, user.Id);
    }

    [Fact]
    public void User_ShouldSet_NameProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Name = "John Doe";

        // Assert
        Assert.Equal("John Doe", user.Name);
    }

    [Fact]
    public void User_ShouldSet_EmailProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Email = "john.doe@example.com";

        // Assert
        Assert.Equal("john.doe@example.com", user.Email);
    }

    [Fact]
    public void User_ShouldSet_PasswordProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Password = "SecurePassword123";

        // Assert
        Assert.Equal("SecurePassword123", user.Password);
    }

    [Fact]
    public void User_ShouldSet_PhoneNoProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.PhoneNo = "1234567890";

        // Assert
        Assert.Equal("1234567890", user.PhoneNo);
    }

    [Fact]
    public void User_ShouldSet_AddressProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Address = "123 Main St";

        // Assert
        Assert.Equal("123 Main St", user.Address);
    }

    [Fact]
    public void User_ShouldSet_GenderProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.Gender = "Male";

        // Assert
        Assert.Equal("Male", user.Gender);
    }

    [Fact]
    public void User_ShouldSet_BirthDateProperty()
    {
        // Arrange
        var user = new User();
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        user.BirthDate = birthDate;

        // Assert
        Assert.Equal(birthDate, user.BirthDate);
    }

    [Fact]
    public void User_ShouldSet_UserTypeProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.UserType = 1;

        // Assert
        Assert.Equal(1, user.UserType);
    }

    [Fact]
    public void User_ShouldSet_IsActiveProperty()
    {
        // Arrange
        var user = new User();

        // Act
        user.IsActive = true;

        // Assert
        Assert.True(user.IsActive);
    }

    [Fact]
    public void User_ShouldSet_CreatedDateProperty()
    {
        // Arrange
        var user = new User();
        var date = DateTime.UtcNow;

        // Act
        user.CreatedDate = date;

        // Assert
        Assert.Equal(date, user.CreatedDate);
    }

    [Fact]
    public void User_ShouldSet_ModifiedDateProperty()
    {
        // Arrange
        var user = new User();
        var date = DateTime.UtcNow;

        // Act
        user.ModifiedDate = date;

        // Assert
        Assert.Equal(date, user.ModifiedDate);
    }

    [Fact]
    public void User_AddressProperty_ShouldAcceptNull()
    {
        // Arrange & Act
        var user = new User { Address = null };

        // Assert
        Assert.Null(user.Address);
    }

    [Fact]
    public void User_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var birthDate = new DateTime(1985, 5, 15);
        var createdDate = DateTime.UtcNow;

        // Act
        var user = new User
        {
            Id = 10,
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            Password = "Password123",
            PhoneNo = "0987654321",
            Address = "456 Oak Ave",
            Gender = "Female",
            BirthDate = birthDate,
            UserType = 2,
            CreatedDate = createdDate,
            ModifiedDate = createdDate,
            IsActive = true,
            CreatedBy = "System",
            ModifiedBy = "Admin"
        };

        // Assert
        Assert.Equal(10, user.Id);
        Assert.Equal("Jane Smith", user.Name);
        Assert.Equal("jane.smith@example.com", user.Email);
        Assert.Equal("Password123", user.Password);
        Assert.Equal("0987654321", user.PhoneNo);
        Assert.Equal("456 Oak Ave", user.Address);
        Assert.Equal("Female", user.Gender);
        Assert.Equal(birthDate, user.BirthDate);
        Assert.Equal(2, user.UserType);
        Assert.True(user.IsActive);
        Assert.Equal(createdDate, user.CreatedDate);
        Assert.Equal(createdDate, user.ModifiedDate);
        Assert.Equal("System", user.CreatedBy);
        Assert.Equal("Admin", user.ModifiedBy);
    }
}
