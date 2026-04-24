namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for authentication operations
/// </summary>
public interface IAuthenticationService
{
    Task<(bool Success, int UserId, int UserType)> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<(bool Success, int PatientId, string Message)> RegisterPatientAsync(string name, string birthDate, string email, string password, string phoneNumber, string gender, string address, CancellationToken cancellationToken = default);
}
