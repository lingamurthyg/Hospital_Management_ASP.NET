using Xunit;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Enums.Tests;

public class GenderTests
{
    [Fact]
    public void Gender_MaleValue_ShouldBeZero()
    {
        // Arrange & Act
        var gender = Gender.Male;

        // Assert
        Assert.Equal(0, (int)gender);
    }

    [Fact]
    public void Gender_FemaleValue_ShouldBeOne()
    {
        // Arrange & Act
        var gender = Gender.Female;

        // Assert
        Assert.Equal(1, (int)gender);
    }

    [Fact]
    public void Gender_OtherValue_ShouldBeTwo()
    {
        // Arrange & Act
        var gender = Gender.Other;

        // Assert
        Assert.Equal(2, (int)gender);
    }

    [Fact]
    public void Gender_AllValues_ShouldBeDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues<Gender>();

        // Assert
        Assert.Equal(3, values.Length);
        Assert.Contains(Gender.Male, values);
        Assert.Contains(Gender.Female, values);
        Assert.Contains(Gender.Other, values);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void Gender_ValidValue_ShouldBeDefined(Gender gender)
    {
        // Arrange & Act
        var isDefined = Enum.IsDefined(typeof(Gender), gender);

        // Assert
        Assert.True(isDefined);
    }
}
