using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface ITimeSlotService
{
    Task<IEnumerable<TimeSlotDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TimeSlotDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
