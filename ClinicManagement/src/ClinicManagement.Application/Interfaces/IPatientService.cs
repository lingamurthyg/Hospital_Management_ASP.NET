using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PatientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
