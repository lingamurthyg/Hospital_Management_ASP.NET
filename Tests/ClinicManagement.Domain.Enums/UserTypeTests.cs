using Xunit;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Enums.Tests;

public class UserTypeTests
{
    [Fact]
    public void UserType_PatientValue_ShouldBeOne()
    {
        // Arrange & Act
        var userType = UserType.Patient;

        // Assert
        Assert.Equal(1, (int)userType);
    }

    [Fact]
    public void UserType_DoctorValue_ShouldBeTwo()
    {
        // Arrange & Act
        var userType = UserType.Doctor;

        // Assert
        Assert.Equal(2, (int)userType);
    }

    [Fact]
    public void UserType_AdminValue_ShouldBeThree()
    {
        // Arrange & Act
        var userType = UserType.Admin;

        // Assert
        Assert.Equal(3, (int)userType);
    }

    [Fact]
    public void UserType_StaffValue_ShouldBeFour()
    {
        // Arrange & Act
        var userType = UserType.Staff;

        // Assert
        Assert.Equal(4, (int)userType);
    }

    [Fact]
    public void UserType_AllValues_ShouldBeDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues<UserType>();

        // Assert
        Assert.Equal(4, values.Length);
        Assert.Contains(UserType.Patient, values);
        Assert.Contains(UserType.Doctor, values);
        Assert.Contains(UserType.Admin, values);
        Assert.Contains(UserType.Staff, values);
    }

    [Theory]
    [InlineData(UserType.Patient)]
    [InlineData(UserType.Doctor)]
    [InlineData(UserType.Admin)]
    [InlineData(UserType.Staff)]
    public void UserType_ValidValue_ShouldBeDefined(UserType userType)
    {
        // Arrange & Act
        var isDefined = Enum.IsDefined(typeof(UserType), userType);

        // Assert
        Assert.True(isDefined);
    }
}
