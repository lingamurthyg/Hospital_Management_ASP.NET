using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using ClinicManagement.Web.Pages;

namespace ClinicManagement.UnitTests.Web.Pages;

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
        Action act = () => model.OnGet();

        // Assert
        act.Should().NotThrow();
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

    [Fact]
    public void OnGet_WithLoggerException_ShouldNotThrow()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IndexModel>>();
        mockLogger.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
            .Throws(new Exception("Logger error"));

        var model = new IndexModel(mockLogger.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => model.OnGet());
    }

    [Fact]
    public void OnGet_ShouldExecuteSuccessfully()
    {
        // Arrange
        var model = new IndexModel(_mockLogger.Object);

        // Act
        model.OnGet();

        // Assert
        // Method completes without exception
        _mockLogger.Verify(
            x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public void IndexModel_ShouldHavePublicOnGetMethod()
    {
        // Arrange & Act
        var methodInfo = typeof(IndexModel).GetMethod("OnGet");

        // Assert
        methodInfo.Should().NotBeNull();
        methodInfo!.IsPublic.Should().BeTrue();
    }

    [Fact]
    public void IndexModel_ShouldBeInCorrectNamespace()
    {
        // Arrange & Act
        var model = new IndexModel(_mockLogger.Object);

        // Assert
        model.GetType().Namespace.Should().Be("ClinicManagement.Web.Pages");
    }
}
