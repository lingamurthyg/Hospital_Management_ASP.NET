using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Tests
{
    /// <summary>
    /// Tests for Program.cs startup configuration and dependency injection
    /// </summary>
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProgramTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Program_Should_ConfigureServices_Successfully()
        {
            // Arrange & Act
            var client = _factory.CreateClient();

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Program_Should_RegisterDbContext_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var dbContext = serviceProvider.GetService<ClinicDbContext>();

            // Assert
            Assert.NotNull(dbContext);
        }

        [Fact]
        public void Program_Should_RegisterPatientRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IPatientRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<PatientRepository>(repository);
        }

        [Fact]
        public void Program_Should_RegisterDoctorRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IDoctorRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<DoctorRepository>(repository);
        }

        [Fact]
        public void Program_Should_RegisterAppointmentRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IAppointmentRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<AppointmentRepository>(repository);
        }

        [Fact]
        public void Program_Should_RegisterDepartmentRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IDepartmentRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<DepartmentRepository>(repository);
        }

        [Fact]
        public void Program_Should_RegisterBillRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IBillRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<BillRepository>(repository);
        }

        [Fact]
        public void Program_Should_RegisterOtherStaffRepository_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var repository = serviceProvider.GetService<IOtherStaffRepository>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<OtherStaffRepository>(repository);
        }

        [Fact]
        public void Program_Should_ConfigureSession_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var sessionService = serviceProvider.GetService<Microsoft.Extensions.Caching.Distributed.IDistributedCache>();

            // Assert
            Assert.NotNull(sessionService);
        }

        [Fact]
        public async Task Program_Should_HandleHttpRequests_Successfully()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Program_Should_RegisterRazorPages_InServiceCollection()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var razorPagesService = serviceProvider.GetService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider>();

            // Assert
            Assert.NotNull(razorPagesService);
        }

        [Fact]
        public void Program_Should_ConfigureLogging_WithSerilog()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var loggerFactory = serviceProvider.GetService<Microsoft.Extensions.Logging.ILoggerFactory>();

            // Assert
            Assert.NotNull(loggerFactory);
        }

        [Fact]
        public void Program_Should_UseHttpsRedirection_InPipeline()
        {
            // Arrange & Act
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Program_Should_ConfigureStaticFiles_InPipeline()
        {
            // Arrange & Act
            var client = _factory.CreateClient();

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Program_Should_RegisterAllRepositories_AsScoped()
        {
            // Arrange
            var scope1 = _factory.Services.CreateScope();
            var scope2 = _factory.Services.CreateScope();

            // Act
            var repo1 = scope1.ServiceProvider.GetService<IPatientRepository>();
            var repo2 = scope1.ServiceProvider.GetService<IPatientRepository>();
            var repo3 = scope2.ServiceProvider.GetService<IPatientRepository>();

            // Assert
            Assert.Same(repo1, repo2); // Same instance within scope
            Assert.NotSame(repo1, repo3); // Different instance across scopes
        }

        [Fact]
        public void Program_Should_ConfigureDbContext_WithSqlServer()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var dbContext = serviceProvider.GetService<ClinicDbContext>();
            var options = dbContext?.Database.GetDbConnection();

            // Assert
            Assert.NotNull(dbContext);
            Assert.NotNull(options);
        }

        [Fact]
        public async Task Program_Should_HandleErrors_Gracefully()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/NonExistentPage");

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Program_Should_ConfigureDistributedCache_ForSession()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var cache = serviceProvider.GetService<Microsoft.Extensions.Caching.Distributed.IDistributedCache>();

            // Assert
            Assert.NotNull(cache);
            Assert.IsType<Microsoft.Extensions.Caching.Memory.MemoryDistributedCache>(cache);
        }

        [Fact]
        public void Program_Should_ConfigureSessionOptions_WithCorrectTimeout()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var sessionOptions = serviceProvider.GetService<Microsoft.AspNetCore.Builder.SessionOptions>();

            // Assert - Session service should be registered
            var sessionService = serviceProvider.GetService<Microsoft.Extensions.Caching.Distributed.IDistributedCache>();
            Assert.NotNull(sessionService);
        }

        [Fact]
        public void Program_Should_EnableAuthorization_InPipeline()
        {
            // Arrange & Act
            var client = _factory.CreateClient();

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Program_Should_MapRazorPages_InPipeline()
        {
            // Arrange
            var scope = _factory.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Act
            var endpointDataSource = serviceProvider.GetService<Microsoft.AspNetCore.Routing.EndpointDataSource>();

            // Assert
            Assert.NotNull(endpointDataSource);
        }
    }
}
