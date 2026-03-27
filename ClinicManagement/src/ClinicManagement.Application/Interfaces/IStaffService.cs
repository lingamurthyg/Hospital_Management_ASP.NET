using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IStaffService
{
    Task<IEnumerable<StaffDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StaffDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
