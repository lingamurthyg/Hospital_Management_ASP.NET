using System.Diagnostics;
using ClinicManagement.Web.Pages;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ClinicManagement.UnitTests.Web.Pages;

public class ErrorModelTests
{
    private readonly Mock<ILogger<ErrorModel>> _mockLogger;
    private readonly ErrorModel _sut;

    public ErrorModelTests()
    {
        _mockLogger = new Mock<ILogger<ErrorModel>>();
        _sut = new ErrorModel(_mockLogger.Object);
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
    public void RequestId_DefaultValue_ShouldBeNull()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);

        // Act & Assert
        model.RequestId.Should().BeNull();
    }

    [Fact]
    public void RequestId_SetValue_ShouldReturnSetValue()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var expectedRequestId = "test-request-id-123";

        // Act
        model.RequestId = expectedRequestId;

        // Assert
        model.RequestId.Should().Be(expectedRequestId);
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
    public void ShowRequestId_WhenRequestIdIsWhitespace_ShouldReturnTrue()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = "   "
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeTrue(); // string.IsNullOrEmpty doesn't treat whitespace as empty
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
    public void OnGet_WithActiveActivity_ShouldSetRequestIdFromActivity()
    {
        // Arrange
        var activity = new Activity("TestActivity");
        activity.Start();
        
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        try
        {
            // Act
            model.OnGet();

            // Assert
            model.RequestId.Should().NotBeNullOrEmpty();
            model.RequestId.Should().Be(activity.Id);
        }
        finally
        {
            activity.Stop();
        }
    }

    [Fact]
    public void OnGet_WithoutActiveActivity_ShouldSetRequestIdFromHttpContext()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        var expectedTraceIdentifier = "trace-id-12345";
        httpContext.TraceIdentifier = expectedTraceIdentifier;
        
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().Be(expectedTraceIdentifier);
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutException()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object);
        var httpContext = new DefaultHttpContext();
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        var exception = Record.Exception(() => model.OnGet());

        // Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void ErrorModel_ShouldHaveResponseCacheAttribute()
    {
        // Arrange & Act
        var attribute = typeof(ErrorModel)
            .GetCustomAttributes(typeof(ResponseCacheAttribute), false)
            .FirstOrDefault() as ResponseCacheAttribute;

        // Assert
        attribute.Should().NotBeNull();
        attribute!.Duration.Should().Be(0);
        attribute.Location.Should().Be(ResponseCacheLocation.None);
        attribute.NoStore.Should().BeTrue();
    }

    [Fact]
    public void ErrorModel_ShouldHaveIgnoreAntiforgeryTokenAttribute()
    {
        // Arrange & Act
        var attribute = typeof(ErrorModel)
            .GetCustomAttributes(typeof(IgnoreAntiforgeryTokenAttribute), false)
            .FirstOrDefault();

        // Assert
        attribute.Should().NotBeNull();
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
        httpContext.TraceIdentifier = "trace-1";
        model.OnGet();
        var firstRequestId = model.RequestId;

        httpContext.TraceIdentifier = "trace-2";
        model.OnGet();
        var secondRequestId = model.RequestId;

        // Assert
        firstRequestId.Should().Be("trace-1");
        secondRequestId.Should().Be("trace-2");
        firstRequestId.Should().NotBe(secondRequestId);
    }

    [Fact]
    public void RequestId_SetToNull_ShowRequestIdShouldBeFalse()
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = "initial-value"
        };

        // Act
        model.RequestId = null;

        // Assert
        model.ShowRequestId.Should().BeFalse();
    }

    [Theory]
    [InlineData("request-id-1")]
    [InlineData("abc123")]
    [InlineData("00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01")]
    public void ShowRequestId_WithVariousValidRequestIds_ShouldReturnTrue(string requestId)
    {
        // Arrange
        var model = new ErrorModel(_mockLogger.Object)
        {
            RequestId = requestId
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeTrue();
    }
}
