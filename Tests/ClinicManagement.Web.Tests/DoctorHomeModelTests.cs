using Xunit;
using Moq;
using ClinicManagement.Web.Pages.Doctor;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ClinicManagement.Web.Tests.Pages;

public class DoctorHomeModelTests
{
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly HomeModel _pageModel;

    public DoctorHomeModelTests()
    {
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _pageModel = new HomeModel(_mockDoctorRepository.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void DoctorHomeModel_Constructor_InitializesProperties()
    {
        // Arrange & Act
        var model = new HomeModel(_mockDoctorRepository.Object);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(string.Empty, model.DoctorName);
    }

    [Fact]
    public async Task OnGetAsync_WithValidSession_LoadsDoctorName()
    {
        // Arrange
        _pageModel.HttpContext.Session.SetInt32("UserId", 1);
        var doctor = new Doctor { Id = 1, Name = "Dr. Smith", IsActive = true };
        _mockDoctorRepository.Setup(x => x.GetByIdAsync(1, default)).ReturnsAsync(doctor);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal("Dr. Smith", _pageModel.DoctorName);
    }

    [Fact]
    public async Task OnGetAsync_WithoutSession_RedirectsToLogin()
    {
        // Arrange - No session set

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Account/Login", redirectResult.PageName);
    }

    [Fact]
    public async Task OnGetAsync_WithNonExistentDoctor_ReturnsPage()
    {
        // Arrange
        _pageModel.HttpContext.Session.SetInt32("UserId", 999);
        _mockDoctorRepository.Setup(x => x.GetByIdAsync(999, default)).ReturnsAsync((Doctor?)null);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(string.Empty, _pageModel.DoctorName);
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
