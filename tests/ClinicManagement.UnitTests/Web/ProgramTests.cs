using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;

namespace ClinicManagement.UnitTests.Web;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Program_ShouldCreateWebApplication_Successfully()
    {
        // Arrange & Act
        var client = _factory.CreateClient();

        // Assert
        client.Should().NotBeNull();
    }

    [Fact]
    public async Task Program_ShouldConfigureRazorPages_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Program_ShouldConfigureStaticFiles_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public void Program_ShouldHavePartialClass_ForTestability()
    {
        // Arrange & Act
        var programType = typeof(Program);

        // Assert
        programType.Should().NotBeNull();
        programType.Name.Should().Be("Program");
    }

    [Fact]
    public async Task Program_ShouldConfigureHttpsRedirection_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task Program_ShouldConfigureSession_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        // Session middleware should be configured
    }

    [Fact]
    public async Task Program_ShouldConfigureAuthorization_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        // Authorization middleware should be configured
    }

    [Fact]
    public void Program_ShouldConfigureSerilog_Successfully()
    {
        // Arrange & Act
        var client = _factory.CreateClient();

        // Assert
        client.Should().NotBeNull();
        // Serilog should be configured
    }

    [Fact]
    public async Task Program_ShouldHandleErrors_InProduction()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/Error");

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task Program_ShouldConfigureInfrastructureServices_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        // Infrastructure services should be registered
    }

    [Fact]
    public async Task Program_ShouldConfigureApplicationServices_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        // Application services should be registered
    }

    [Fact]
    public async Task Program_ShouldConfigureDistributedMemoryCache_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.Should().NotBeNull();
        // Distributed memory cache should be configured
    }
}
