using Xunit;
using ClinicManagement.Web.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ClinicManagement.Web.Tests.Pages;

public class AdminHomeModelTests
{
    private readonly HomeModel _pageModel;

    public AdminHomeModelTests()
    {
        _pageModel = new HomeModel();

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void AdminHomeModel_Constructor_CreatesInstance()
    {
        // Arrange & Act
        var model = new HomeModel();

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public void OnGet_WithValidSession_ReturnsPage()
    {
        // Arrange
        _pageModel.HttpContext.Session.SetInt32("UserId", 1);

        // Act
        var result = _pageModel.OnGet();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public void OnGet_WithoutSession_RedirectsToLogin()
    {
        // Arrange - No session set

        // Act
        var result = _pageModel.OnGet();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Account/Login", redirectResult.PageName);
    }

    [Fact]
    public void AdminHomeModel_InheritsFromPageModel()
    {
        // Arrange & Act
        var model = new HomeModel();

        // Assert
        Assert.IsAssignableFrom<PageModel>(model);
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
