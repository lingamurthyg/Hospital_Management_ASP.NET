using ClinicManagement.Web.Pages;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicManagement.UnitTests.Web.Pages;

public class IndexModelTests
{
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _sut;

    public IndexModelTests()
    {
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _sut = new IndexModel(_mockLogger.Object);
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
    public void OnGet_ShouldLogInformation()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(loggerMock.Object);

        // Act
        model.OnGet();

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home page accessed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutException()
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
        var loggerMock = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(loggerMock.Object);

        // Act
        model.OnGet();
        model.OnGet();
        model.OnGet();

        // Assert
        loggerMock.Verify(
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

    [Fact]
    public void OnGet_WithLoggerThatThrows_ShouldPropagateException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<IndexModel>>();
        loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
            .Throws<InvalidOperationException>();

        var model = new IndexModel(loggerMock.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => model.OnGet());
    }
}
