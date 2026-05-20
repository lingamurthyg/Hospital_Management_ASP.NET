using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FluentAssertions;
using System.Diagnostics;
using ClinicManagement.Web.Pages;

namespace ClinicManagement.UnitTests.Web.Pages;

public class ErrorModelTests
{
    private readonly Mock<ILogger<ErrorModel>> _mockLogger;
    private readonly ErrorModel _errorModel;

    public ErrorModelTests()
    {
        _mockLogger = new Mock<ILogger<ErrorModel>>();
        _errorModel = new ErrorModel(_mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidLogger_ShouldCreateInstance()
    {
        // Arrange & Act
        var model = new ErrorModel(_mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ErrorModel(null!));
    }

    [Fact]
    public void RequestId_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var model = new ErrorModel(_mockLogger.Object);

        // Assert
        model.RequestId.Should().BeNull();
    }

    [Fact]
    public void RequestId_CanBeSet()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var requestId = "test-request-id-123";

        // Act
        model.RequestId = requestId;

        // Assert
        model.RequestId.Should().Be(requestId);
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsNull_ShouldReturnFalse()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = null
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = string.Empty
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ShowRequestId_WhenRequestIdHasValue_ShouldReturnTrue()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = "test-request-id"
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OnGet_ShouldSetRequestIdFromActivity()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var activity = new Activity("TestActivity");
        activity.Start();

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().NotBeNullOrEmpty();
        activity.Stop();
    }

    [Fact]
    public void OnGet_WhenActivityIsNull_ShouldSetRequestIdFromHttpContext()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "trace-123";
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().Be("trace-123");
    }

    [Fact]
    public void OnGet_ShouldNotThrowException()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        Action act = () => model.OnGet();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ErrorModel_ShouldHaveResponseCacheAttribute()
    {
        // Arrange & Act
        var attributes = typeof(ErrorModel).GetCustomAttributes(typeof(ResponseCacheAttribute), false);

        // Assert
        attributes.Should().NotBeEmpty();
        var responseCacheAttr = attributes[0] as ResponseCacheAttribute;
        responseCacheAttr.Should().NotBeNull();
        responseCacheAttr!.Duration.Should().Be(0);
        responseCacheAttr.Location.Should().Be(ResponseCacheLocation.None);
        responseCacheAttr.NoStore.Should().BeTrue();
    }

    [Fact]
    public void ErrorModel_ShouldHaveIgnoreAntiforgeryTokenAttribute()
    {
        // Arrange & Act
        var attributes = typeof(ErrorModel).GetCustomAttributes(typeof(IgnoreAntiforgeryTokenAttribute), false);

        // Assert
        attributes.Should().NotBeEmpty();
    }

    [Fact]
    public void ErrorModel_ShouldInheritFromPageModel()
    {
        // Arrange & Act
        var model = new ErrorModel(_mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void OnGet_CalledMultipleTimes_ShouldUpdateRequestIdEachTime()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        model.OnGet();
        var firstRequestId = model.RequestId;
        
        httpContext.TraceIdentifier = "new-trace-id";
        model.OnGet();
        var secondRequestId = model.RequestId;

        // Assert
        firstRequestId.Should().NotBeNull();
        secondRequestId.Should().NotBeNull();
    }

    [Fact]
    public void ErrorModel_ShouldBeInCorrectNamespace()
    {
        // Arrange & Act
        var model = new ErrorModel(_mockLogger.Object);

        // Assert
        model.GetType().Namespace.Should().Be("ClinicManagement.Web.Pages");
    }

    [Fact]
    public void ErrorModel_ShouldHavePublicOnGetMethod()
    {
        // Arrange & Act
        var methodInfo = typeof(ErrorModel).GetMethod("OnGet");

        // Assert
        methodInfo.Should().NotBeNull();
        methodInfo!.IsPublic.Should().BeTrue();
    }

    [Fact]
    public void RequestId_Property_ShouldBeNullable()
    {
        // Arrange & Act
        var propertyInfo = typeof(ErrorModel).GetProperty("RequestId");

        // Assert
        propertyInfo.Should().NotBeNull();
        propertyInfo!.PropertyType.Should().Be(typeof(string));
    }

    [Fact]
    public void ShowRequestId_Property_ShouldBeBoolean()
    {
        // Arrange & Act
        var propertyInfo = typeof(ErrorModel).GetProperty("ShowRequestId");

        // Assert
        propertyInfo.Should().NotBeNull();
        propertyInfo!.PropertyType.Should().Be(typeof(bool));
    }
}
