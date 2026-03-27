using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IBillService
{
    Task<IEnumerable<BillDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BillDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
