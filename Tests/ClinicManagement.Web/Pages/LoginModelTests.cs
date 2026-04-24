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

public class LoginModelTests
{
    private readonly Mock<IAuthenticationService> _mockAuthService;
    private readonly Mock<ILogger<LoginModel>> _mockLogger;
    private readonly LoginModel _pageModel;

    public LoginModelTests()
    {
        _mockAuthService = new Mock<IAuthenticationService>();
        _mockLogger = new Mock<ILogger<LoginModel>>();
        _pageModel = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void LoginModel_Constructor_InitializesProperties()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(model);
        Assert.NotNull(model.Input);
    }

    [Fact]
    public void LoginModel_OnGet_ExecutesWithoutError()
    {
        // Arrange
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Act
        model.OnGet();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task OnPostAsync_WithValidPatientCredentials_RedirectsToPatientHome()
    {
        // Arrange
        _pageModel.Input = new LoginViewModel
        {
            Email = "patient@test.com",
            Password = "password"
        };

        _mockAuthService.Setup(x => x.ValidateLoginAsync("patient@test.com", "password", default))
            .ReturnsAsync((true, 1, 1));

        _pageModel.ModelState.Clear();

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Patient/Home", redirectResult.PageName);
    }

    [Fact]
    public async Task OnPostAsync_WithValidDoctorCredentials_RedirectsToDoctorHome()
    {
        // Arrange
        _pageModel.Input = new LoginViewModel
        {
            Email = "doctor@test.com",
            Password = "password"
        };

        _mockAuthService.Setup(x => x.ValidateLoginAsync("doctor@test.com", "password", default))
            .ReturnsAsync((true, 2, 2));

        _pageModel.ModelState.Clear();

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Doctor/Home", redirectResult.PageName);
    }

    [Fact]
    public async Task OnPostAsync_WithValidAdminCredentials_RedirectsToAdminHome()
    {
        // Arrange
        _pageModel.Input = new LoginViewModel
        {
            Email = "admin@test.com",
            Password = "password"
        };

        _mockAuthService.Setup(x => x.ValidateLoginAsync("admin@test.com", "password", default))
            .ReturnsAsync((true, 3, 3));

        _pageModel.ModelState.Clear();

        // Act
        var result = await _pageModel.OnPostAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Admin/Home", redirectResult.PageName);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidCredentials_ReturnsPageWithError()
    {
        // Arrange
        _pageModel.Input = new LoginViewModel
        {
            Email = "invalid@test.com",
            Password = "wrongpassword"
        };

        _mockAuthService.Setup(x => x.ValidateLoginAsync("invalid@test.com", "wrongpassword", default))
            .ReturnsAsync((false, 0, 0));

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
