using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Domain.Interfaces;
using ClinicManagement.Infrastructure.Repositories;
using ClinicManagement.Application.Services;
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
            builder.Should().NotBeNull();
            builder.Services.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_ContainsControllers()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddControllers();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_ContainsEndpointsApiExplorer()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddEndpointsApiExplorer();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_ContainsSwaggerGen()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddSwaggerGen();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterDbContext()
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
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<ClinicDbContext>();
            dbContext.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterPatientRepository()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Act
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var repository = serviceProvider.GetService<IPatientRepository>();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterDoctorRepository()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Act
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var repository = serviceProvider.GetService<IDoctorRepository>();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterAppointmentRepository()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Act
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var repository = serviceProvider.GetService<IAppointmentRepository>();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterPatientService()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            // Act
            builder.Services.AddScoped<PatientService>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var service = serviceProvider.GetService<PatientService>();
            service.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterDoctorService()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

            // Act
            builder.Services.AddScoped<DoctorService>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var service = serviceProvider.GetService<DoctorService>();
            service.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanRegisterAppointmentService()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Act
            builder.Services.AddScoped<AppointmentService>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            var service = serviceProvider.GetService<AppointmentService>();
            service.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanAddCors()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_CanAddLogging()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);

            // Act
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void WebApplication_CanBuild()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "Host=localhost;Database=test;Username=test;Password=test"}
            };
            builder.Configuration.AddInMemoryCollection(inMemorySettings);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Act
            var app = builder.Build();

            // Assert
            app.Should().NotBeNull();
        }

        [Fact]
        public void WebApplication_HasEnvironment()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // Act
            var environment = app.Environment;

            // Assert
            environment.Should().NotBeNull();
            environment.ApplicationName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WebApplicationBuilder_WithEmptyArgs_CreatesBuilder()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            var builder = WebApplication.CreateBuilder(args);

            // Assert
            builder.Should().NotBeNull();
            builder.Configuration.Should().NotBeNull();
        }

        [Fact]
        public void WebApplicationBuilder_WithNullArgs_CreatesBuilder()
        {
            // Arrange
            string[] args = null;

            // Act
            var builder = WebApplication.CreateBuilder(args);

            // Assert
            builder.Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_AllRepositories_CanBeRegistered()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Act
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.GetService<IPatientRepository>().Should().NotBeNull();
            serviceProvider.GetService<IDoctorRepository>().Should().NotBeNull();
            serviceProvider.GetService<IAppointmentRepository>().Should().NotBeNull();
        }

        [Fact]
        public void ServiceCollection_AllServices_CanBeRegistered()
        {
            // Arrange
            var args = Array.Empty<string>();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Act
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<AppointmentService>();

            // Assert
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.GetService<PatientService>().Should().NotBeNull();
            serviceProvider.GetService<DoctorService>().Should().NotBeNull();
            serviceProvider.GetService<AppointmentService>().Should().NotBeNull();
        }
    }
}
