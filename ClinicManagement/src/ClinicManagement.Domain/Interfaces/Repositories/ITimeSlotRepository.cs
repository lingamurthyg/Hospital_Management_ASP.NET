using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Interfaces.Repositories;

public interface ITimeSlotRepository
{
    Task<IEnumerable<TimeSlot>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TimeSlot?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlot>> GetAvailableByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<TimeSlot> AddAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default);
    Task UpdateAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
