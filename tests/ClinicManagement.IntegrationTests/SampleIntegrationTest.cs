using Xunit;

namespace ClinicManagement.IntegrationTests;

public class SampleIntegrationTest
{
    [Fact]
    public void Sample_Integration_Test_Should_Pass()
    {
        // Arrange
        var expected = true;

        // Act
        var actual = true;

        // Assert
        Assert.Equal(expected, actual);
    }
}
