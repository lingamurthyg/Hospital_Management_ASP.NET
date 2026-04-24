using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for Admin entity operations
/// </summary>
public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Admin?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default);
}
