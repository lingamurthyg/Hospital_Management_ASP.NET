using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
