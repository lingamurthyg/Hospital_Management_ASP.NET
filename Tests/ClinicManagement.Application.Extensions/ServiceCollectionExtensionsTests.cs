using Xunit;
using ClinicManagement.Application.Extensions;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Application.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddApplicationServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IServiceCollection>(result);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIPatientService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IPatientService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIDoctorService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDoctorService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIDepartmentService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDepartmentService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIAppointmentService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IAppointmentService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterITimeSlotService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(ITimeSlotService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIBillService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IBillService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIFeedbackService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IFeedbackService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIStaffService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IStaffService));
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterIAuthenticationService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IAuthenticationService));
    }

    [Fact]
    public void AddApplicationServices_AllServices_ShouldBeRegisteredAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();

        // Assert
        var applicationServices = services.Where(s => s.ServiceType.Name.EndsWith("Service")).ToList();
        Assert.All(applicationServices, s => Assert.Equal(ServiceLifetime.Scoped, s.Lifetime));
    }
}
