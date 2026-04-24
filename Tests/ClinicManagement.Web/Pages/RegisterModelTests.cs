using Xunit;
using Moq;
using ClinicManagement.Web.Pages.Account;
using ClinicManagement.Domain.Interfaces.Services;
using ClinicManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ClinicManagement.Web.Tests.Pages;

public class RegisterModelTests
{
    private readonly Mock<IAuthenticationService> _mockAuthService;
    private readonly Mock<ILogger<RegisterModel>> _mockLogger;
    private readonly RegisterModel _pageModel;

    public RegisterModelTests()
    {
        _mockAuthService = new Mock<IAuthenticationService>();
        _mockLogger = new Mock<ILogger<RegisterModel>>();
        _pageModel = new RegisterModel(_mockAuthService.Object, _mockLogger.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void RegisterModel_Constructor_InitializesProperties()
    {
        // Arrange & Act
        var model = new RegisterModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Input);
    }

    [Fact]
    public void RegisterModel_OnGet_ExecutesWithoutError()
    {
        // Arrange
        var model = new RegisterModel(_mockAuthService.Object, _mockLogger.Object);

        // Act
        model.OnGet();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task OnPostAsync_WithValidData_RedirectsToPatientHome()
    {
        // Arrange
        _pageModel.Input = new RegisterViewModel
        {
            Name = "Test User",
            BirthDate = "1990-01-01",
            Email = "test@example.com",
            Password = "Password123",
            PhoneNumber = "555-1234",
            Gender = "Male",
            Address = "123 Test St"
        };

        _mockAuthService.Setup(x => x.RegisterPatientAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<string>(), default))
            .ReturnsAsync((true, 1, "Success"));

        _pageModel.ModelState.Clear();

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Patient/Home", redirectResult.PageName);
    }

    [Fact]
    public async Task OnPostAsync_WithEmailExists_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Input = new RegisterViewModel
        {
            Name = "Test User",
            BirthDate = "1990-01-01",
            Email = "existing@example.com",
            Password = "Password123",
            PhoneNumber = "555-1234",
            Gender = "Male",
            Address = "123 Test St"
        };

        _mockAuthService.Setup(x => x.RegisterPatientAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<string>(), default))
            .ReturnsAsync((false, 0, "Email already exists"));

        _pageModel.ModelState.Clear();

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(_pageModel.ModelState.IsValid);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidModelState_ReturnsPage()
    {
        // Arrange
        _pageModel.ModelState.AddModelError("Email", "Email is required");

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    private class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _storage = new();

        public bool IsAvailable => true;
        public string Id => "test-session";
        public IEnumerable<string> Keys => _storage.Keys;

        public void Clear() => _storage.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => _storage.Remove(key);
        public void Set(string key, byte[] value) => _storage[key] = value;
        public bool TryGetValue(string key, out byte[]? value) => _storage.TryGetValue(key, out value);
    }
}
