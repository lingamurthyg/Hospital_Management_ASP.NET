using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
