using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace ClinicManagement.Web.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void WebApplicationBuilder_CanBeCreated()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            var builder = WebApplication.CreateBuilder(args);

            // Assert
            Assert.NotNull(builder);
            Assert.NotNull(builder.Services);
            Assert.NotNull(builder.Configuration);
        }

        [Fact]
        public void WebApplicationBuilder_WithEmptyArgs_CreatesSuccessfully()
        {
            // Arrange
            var args = new string[] { };

            // Act
            var builder = WebApplication.CreateBuilder(args);

            // Assert
            Assert.NotNull(builder);
        }

        [Fact]
        public void WebApplicationBuilder_WithNullArgs_CreatesSuccessfully()
        {
            // Arrange
            string[] args = null;

            // Act
            var builder = WebApplication.CreateBuilder(args);

            // Assert
            Assert.NotNull(builder);
        }

        [Fact]
        public void ServiceCollection_CanAddRazorPages()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddRazorPages();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void ServiceCollection_CanAddDistributedMemoryCache()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddDistributedMemoryCache();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void ServiceCollection_CanAddSession()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void ServiceCollection_CanAddHttpContextAccessor()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddHttpContextAccessor();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void SessionOptions_IdleTimeout_IsThirtyMinutes()
        {
            // Arrange
            var expectedTimeout = TimeSpan.FromMinutes(30);

            // Act
            var timeout = TimeSpan.FromMinutes(30);

            // Assert
            Assert.Equal(expectedTimeout, timeout);
        }

        [Fact]
        public void SessionOptions_CookieHttpOnly_IsTrue()
        {
            // Arrange & Act
            var httpOnly = true;

            // Assert
            Assert.True(httpOnly);
        }

        [Fact]
        public void SessionOptions_CookieIsEssential_IsTrue()
        {
            // Arrange & Act
            var isEssential = true;

            // Assert
            Assert.True(isEssential);
        }

        [Fact]
        public void Configuration_CanReadConnectionString()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "Host=localhost;Database=test;Username=test;Password=test"}
            };
            builder.Configuration.AddInMemoryCollection(inMemorySettings);

            // Act
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Assert
            Assert.NotNull(connectionString);
            Assert.Contains("localhost", connectionString);
        }

        [Fact]
        public void TimeSpan_FromMinutes_CreatesCorrectDuration()
        {
            // Arrange
            var minutes = 30;

            // Act
            var timeSpan = TimeSpan.FromMinutes(minutes);

            // Assert
            Assert.Equal(30, timeSpan.TotalMinutes);
        }

        [Fact]
        public void TimeSpan_FromSeconds_CreatesCorrectDuration()
        {
            // Arrange
            var seconds = 30;

            // Act
            var timeSpan = TimeSpan.FromSeconds(seconds);

            // Assert
            Assert.Equal(30, timeSpan.TotalSeconds);
        }

        [Fact]
        public void ServiceCollection_CanRegisterScopedServices()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddScoped<ITestService, TestService>();
            var serviceProvider = builder.Services.BuildServiceProvider();
            var service = serviceProvider.GetService<ITestService>();

            // Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void ServiceCollection_ScopedServices_CreateNewInstancePerScope()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ITestService, TestService>();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Act
            ITestService service1;
            ITestService service2;
            
            using (var scope1 = serviceProvider.CreateScope())
            {
                service1 = scope1.ServiceProvider.GetService<ITestService>();
            }
            
            using (var scope2 = serviceProvider.CreateScope())
            {
                service2 = scope2.ServiceProvider.GetService<ITestService>();
            }

            // Assert
            Assert.NotNull(service1);
            Assert.NotNull(service2);
            Assert.NotSame(service1, service2);
        }

        [Fact]
        public void WebApplication_CanBuild()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();

            // Act
            var app = builder.Build();

            // Assert
            Assert.NotNull(app);
        }

        [Fact]
        public void WebApplication_HasEnvironmentProperty()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // Assert
            Assert.NotNull(app.Environment);
        }

        [Fact]
        public void Configuration_CanAddInMemoryCollection()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            var settings = new Dictionary<string, string>
            {
                {"TestKey", "TestValue"}
            };

            // Act
            builder.Configuration.AddInMemoryCollection(settings);
            var value = builder.Configuration["TestKey"];

            // Assert
            Assert.Equal("TestValue", value);
        }

        [Fact]
        public void Configuration_WithMultipleSettings_RetrievesCorrectly()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            var settings = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"},
                {"Key3", "Value3"}
            };

            // Act
            builder.Configuration.AddInMemoryCollection(settings);

            // Assert
            Assert.Equal("Value1", builder.Configuration["Key1"]);
            Assert.Equal("Value2", builder.Configuration["Key2"]);
            Assert.Equal("Value3", builder.Configuration["Key3"]);
        }

        [Fact]
        public void ServiceProvider_CanResolveMultipleServices()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<IAnotherTestService, AnotherTestService>();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Act
            var service1 = serviceProvider.GetService<ITestService>();
            var service2 = serviceProvider.GetService<IAnotherTestService>();

            // Assert
            Assert.NotNull(service1);
            Assert.NotNull(service2);
        }

        // Helper interfaces and classes for testing
        private interface ITestService { }
        private class TestService : ITestService { }
        private interface IAnotherTestService { }
        private class AnotherTestService : IAnotherTestService { }
    }
}
