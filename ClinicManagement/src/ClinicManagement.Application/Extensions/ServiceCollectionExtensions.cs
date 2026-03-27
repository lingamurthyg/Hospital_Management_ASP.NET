using ClinicManagement.Application.Mappings;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<ITimeSlotService, TimeSlotService>();
        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
