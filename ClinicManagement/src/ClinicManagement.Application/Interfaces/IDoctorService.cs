using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
