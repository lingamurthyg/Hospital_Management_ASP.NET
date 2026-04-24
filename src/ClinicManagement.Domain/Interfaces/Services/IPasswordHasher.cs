namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Interface for password hashing operations
/// </summary>
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
