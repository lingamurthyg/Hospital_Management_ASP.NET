using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Application.Extensions;

/// <summary>
/// Extension methods for configuring application services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Register services
        services.AddScoped<IPatientService, Services.PatientService>();
        services.AddScoped<IDoctorService, Services.DoctorService>();
        services.AddScoped<IDepartmentService, Services.DepartmentService>();
        services.AddScoped<IAppointmentService, Services.AppointmentService>();
        services.AddScoped<ITimeSlotService, Services.TimeSlotService>();
        services.AddScoped<IBillService, Services.BillService>();
        services.AddScoped<IOtherStaffService, Services.OtherStaffService>();

        return services;
    }
}
