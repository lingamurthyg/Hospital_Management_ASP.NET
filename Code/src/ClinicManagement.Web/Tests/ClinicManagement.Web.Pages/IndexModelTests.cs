using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using ClinicManagement.Web.Pages;
using FluentAssertions;

namespace ClinicManagement.Web.Tests.Pages;

public class IndexModelTests
{
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _indexModel;

    public IndexModelTests()
    {
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _indexModel = new IndexModel(_mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidLogger_ShouldCreateInstance()
    {
        // Arrange & Act
        var model = new IndexModel(_mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IndexModel(null!));
    }

    [Fact]
    public void OnGet_ShouldLogInformation()
    {
        // Arrange
        var model = new IndexModel(_mockLogger.Object);

        // Act
        model.OnGet();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home page accessed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void OnGet_ShouldNotThrowException()
    {
        // Arrange
        var model = new IndexModel(_mockLogger.Object);

        // Act
        var exception = Record.Exception(() => model.OnGet());

        // Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void OnGet_CalledMultipleTimes_ShouldLogEachTime()
    {
        // Arrange
        var model = new IndexModel(_mockLogger.Object);

        // Act
        model.OnGet();
        model.OnGet();
        model.OnGet();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home page accessed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Exactly(3));
    }

    [Fact]
    public void IndexModel_ShouldInheritFromPageModel()
    {
        // Arrange & Act
        var model = new IndexModel(_mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<Microsoft.AspNetCore.Mvc.RazorPages.PageModel>();
    }
}
