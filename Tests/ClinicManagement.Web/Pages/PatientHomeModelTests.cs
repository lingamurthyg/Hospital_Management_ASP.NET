using Xunit;
using Moq;
using ClinicManagement.Web.Pages.Patient;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ClinicManagement.Web.Tests.Pages;

public class PatientHomeModelTests
{
    private readonly Mock<IPatientRepository> _mockPatientRepository;
    private readonly HomeModel _pageModel;

    public PatientHomeModelTests()
    {
        _mockPatientRepository = new Mock<IPatientRepository>();
        _pageModel = new HomeModel(_mockPatientRepository.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _pageModel.PageContext = new PageContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void PatientHomeModel_Constructor_InitializesProperties()
    {
        // Arrange & Act
        var model = new HomeModel(_mockPatientRepository.Object);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(string.Empty, model.PatientName);
    }

    [Fact]
    public async Task OnGetAsync_WithValidSession_LoadsPatientName()
    {
        // Arrange
        _pageModel.HttpContext.Session.SetInt32("UserId", 1);
        var patient = new Patient { Id = 1, Name = "John Doe", IsActive = true };
        _mockPatientRepository.Setup(x => x.GetByIdAsync(1, default)).ReturnsAsync(patient);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal("John Doe", _pageModel.PatientName);
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
    public async Task OnGetAsync_WithNonExistentPatient_ReturnsPage()
    {
        // Arrange
        _pageModel.HttpContext.Session.SetInt32("UserId", 999);
        _mockPatientRepository.Setup(x => x.GetByIdAsync(999, default)).ReturnsAsync((Patient?)null);

        // Act
        var result = await _pageModel.OnGetAsync();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(string.Empty, _pageModel.PatientName);
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
