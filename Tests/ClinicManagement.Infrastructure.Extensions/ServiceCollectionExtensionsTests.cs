using Xunit;
using ClinicManagement.Infrastructure.Extensions;
using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Infrastructure.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInfrastructureServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddInfrastructureServices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IServiceCollection>(result);
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIPatientRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IPatientRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIDoctorRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDoctorRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIDepartmentRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDepartmentRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIAppointmentRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IAppointmentRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterITimeSlotRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(ITimeSlotRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIBillRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IBillRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIFeedbackRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IFeedbackRepository));
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterIStaffRepository()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IStaffRepository));
    }

    [Fact]
    public void AddInfrastructureServices_AllRepositories_ShouldBeRegisteredAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServices();

        // Assert
        var repositories = services.Where(s => s.ServiceType.Name.EndsWith("Repository")).ToList();
        Assert.All(repositories, r => Assert.Equal(ServiceLifetime.Scoped, r.Lifetime));
    }
}
