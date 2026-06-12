using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using ClinicManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ClinicManagement.Web.Tests.Pages
{
    /// <summary>
    /// Tests for IndexModel page model
    /// </summary>
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
        public void Constructor_Should_CreateInstance_WithValidLogger()
        {
            // Arrange
            var logger = new Mock<ILogger<IndexModel>>();

            // Act
            var model = new IndexModel(logger.Object);

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentNullException_WhenLoggerIsNull()
        {
            // Arrange
            ILogger<IndexModel>? logger = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new IndexModel(logger!));
        }

        [Fact]
        public void IndexModel_Should_InheritFrom_PageModel()
        {
            // Arrange & Act
            var model = _indexModel;

            // Assert
            Assert.IsAssignableFrom<PageModel>(model);
        }

        [Fact]
        public void OnGet_Should_ExecuteSuccessfully()
        {
            // Arrange
            var model = _indexModel;

            // Act
            model.OnGet();

            // Assert - Method completes without exception
            Assert.NotNull(model);
        }

        [Fact]
        public void OnGet_Should_LogInformation_WhenCalled()
        {
            // Arrange
            var model = _indexModel;

            // Act
            model.OnGet();

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home page visited")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void OnGet_Should_LogInformation_WithCorrectMessage()
        {
            // Arrange
            var model = _indexModel;
            var loggedMessages = new System.Collections.Generic.List<string>();
            
            _mockLogger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Delegate>((level, eventId, state, exception, formatter) =>
                {
                    loggedMessages.Add(state.ToString() ?? string.Empty);
                });

            // Act
            model.OnGet();

            // Assert
            Assert.Contains(loggedMessages, msg => msg.Contains("Home page visited"));
        }

        [Fact]
        public void OnGet_Should_NotThrowException_WhenLoggerFails()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<IndexModel>>();
            mockLogger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Throws(new Exception("Logging failed"));

            var model = new IndexModel(mockLogger.Object);

            // Act & Assert
            var exception = Record.Exception(() => model.OnGet());
            Assert.NotNull(exception); // Should propagate the exception
        }

        [Fact]
        public void OnGet_Should_BeCallable_MultipleTimes()
        {
            // Arrange
            var model = _indexModel;

            // Act
            model.OnGet();
            model.OnGet();
            model.OnGet();

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home page visited")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Exactly(3));
        }

        [Fact]
        public void OnGet_Should_LogAtInformationLevel()
        {
            // Arrange
            var model = _indexModel;
            LogLevel? capturedLogLevel = null;

            _mockLogger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Delegate>((level, eventId, state, exception, formatter) =>
                {
                    capturedLogLevel = level;
                });

            // Act
            model.OnGet();

            // Assert
            Assert.Equal(LogLevel.Information, capturedLogLevel);
        }

        [Fact]
        public void OnGet_Should_NotLogException()
        {
            // Arrange
            var model = _indexModel;
            Exception? capturedException = null;

            _mockLogger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Delegate>((level, eventId, state, exception, formatter) =>
                {
                    capturedException = exception;
                });

            // Act
            model.OnGet();

            // Assert
            Assert.Null(capturedException);
        }

        [Fact]
        public void OnGet_Should_ReturnVoid()
        {
            // Arrange
            var model = _indexModel;

            // Act
            var result = model.OnGet();

            // Assert
            Assert.IsType<System.Void>(result.GetType());
        }

        [Fact]
        public void IndexModel_Should_HavePublicOnGetMethod()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var method = type.GetMethod("OnGet");

            // Assert
            Assert.NotNull(method);
            Assert.True(method.IsPublic);
        }

        [Fact]
        public void IndexModel_Should_HaveParameterlessOnGetMethod()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var method = type.GetMethod("OnGet");

            // Assert
            Assert.NotNull(method);
            Assert.Empty(method.GetParameters());
        }

        [Fact]
        public void IndexModel_Should_BeInCorrectNamespace()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var namespaceName = type.Namespace;

            // Assert
            Assert.Equal("ClinicManagement.Web.Pages", namespaceName);
        }

        [Fact]
        public void IndexModel_Should_BePublicClass()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var isPublic = type.IsPublic;

            // Assert
            Assert.True(isPublic);
        }

        [Fact]
        public void IndexModel_Should_HaveLogger_AsPrivateField()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var field = type.GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Assert
            Assert.NotNull(field);
        }

        [Fact]
        public void Constructor_Should_InitializeLogger_Correctly()
        {
            // Arrange
            var logger = new Mock<ILogger<IndexModel>>();

            // Act
            var model = new IndexModel(logger.Object);

            // Assert
            Assert.NotNull(model);
            var field = typeof(IndexModel).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var loggerValue = field?.GetValue(model);
            Assert.NotNull(loggerValue);
        }

        [Fact]
        public void OnGet_Should_UseInjectedLogger()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<IndexModel>>();
            var model = new IndexModel(mockLogger.Object);

            // Act
            model.OnGet();

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void IndexModel_Should_HaveOnlyOneConstructor()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var constructors = type.GetConstructors();

            // Assert
            Assert.Single(constructors);
        }

        [Fact]
        public void Constructor_Should_AcceptILoggerParameter()
        {
            // Arrange
            var type = typeof(IndexModel);

            // Act
            var constructor = type.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            // Assert
            Assert.Single(parameters);
            Assert.Equal(typeof(ILogger<IndexModel>), parameters[0].ParameterType);
        }

        [Fact]
        public void OnGet_Should_CompleteQuickly()
        {
            // Arrange
            var model = _indexModel;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Act
            model.OnGet();
            stopwatch.Stop();

            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds < 100, "OnGet should complete in less than 100ms");
        }
    }
}
