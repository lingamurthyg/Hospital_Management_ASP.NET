using Xunit;

namespace ClinicManagement.UnitTests;

public class SampleTests
{
    [Fact]
    public void Sample_Test_Should_Pass()
    {
        // Arrange
        var expected = true;
        
        // Act
        var actual = true;
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
