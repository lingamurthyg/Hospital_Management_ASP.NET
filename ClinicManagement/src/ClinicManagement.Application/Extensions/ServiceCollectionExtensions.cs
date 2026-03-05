using ClinicManagement.Application.Mappings;
using ClinicManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
