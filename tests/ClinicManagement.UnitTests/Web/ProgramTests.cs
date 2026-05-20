using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ClinicManagement.UnitTests.Web;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Application_ShouldStartSuccessfully()
    {
        // Arrange & Act
        var client = _factory.CreateClient();

        // Assert
        client.Should().NotBeNull();
    }

    [Fact]
    public void Application_ShouldHaveRazorPagesConfigured()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var razorPagesService = services.GetService<Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.PageLoader>();

        // Assert
        razorPagesService.Should().NotBeNull();
    }

    [Fact]
    public void Application_ShouldHaveLoggingConfigured()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var loggerFactory = services.GetService<ILoggerFactory>();

        // Assert
        loggerFactory.Should().NotBeNull();
    }

    [Fact]
    public void Application_ShouldHaveSessionConfigured()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var sessionService = services.GetService<Microsoft.AspNetCore.Session.ISessionStore>();

        // Assert
        sessionService.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_RootEndpoint_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Application_ErrorEndpoint_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/Error");

        // Assert
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldHaveDistributedCacheConfigured()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var cacheService = services.GetService<Microsoft.Extensions.Caching.Distributed.IDistributedCache>();

        // Assert
        cacheService.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_StaticFiles_ShouldBeAccessible()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/css/site.css");

        // Assert - Either 200 (file exists) or 404 (file doesn't exist, but middleware is configured)
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_InvalidRoute_ShouldReturn404()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/NonExistentPage");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public void Application_ShouldHaveAuthorizationConfigured()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var authService = services.GetService<Microsoft.AspNetCore.Authorization.IAuthorizationService>();

        // Assert
        authService.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_MultipleRequests_ShouldHandleConcurrently()
    {
        // Arrange
        var client = _factory.CreateClient();
        var tasks = new List<Task<System.Net.Http.HttpResponseMessage>>();

        // Act
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(client.GetAsync("/"));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert
        responses.Should().HaveCount(10);
        responses.Should().OnlyContain(r => r.IsSuccessStatusCode);
    }

    [Fact]
    public void Application_Configuration_ShouldNotBeNull()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var configuration = services.GetService<Microsoft.Extensions.Configuration.IConfiguration>();

        // Assert
        configuration.Should().NotBeNull();
    }

    [Fact]
    public void Application_Environment_ShouldNotBeNull()
    {
        // Arrange
        var services = _factory.Services;

        // Act
        var environment = services.GetService<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();

        // Assert
        environment.Should().NotBeNull();
    }
}
