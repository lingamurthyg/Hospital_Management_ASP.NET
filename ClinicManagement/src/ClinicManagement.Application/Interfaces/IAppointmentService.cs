using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AppointmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
