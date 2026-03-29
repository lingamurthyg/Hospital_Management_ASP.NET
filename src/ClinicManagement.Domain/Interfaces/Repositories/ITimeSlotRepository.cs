namespace ClinicManagement.Domain.Interfaces.Repositories;

using ClinicManagement.Domain.Entities;

/// <summary>
/// Repository interface for TimeSlot entity operations
/// </summary>
public interface ITimeSlotRepository
{
    Task<IEnumerable<TimeSlot>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TimeSlot?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TimeSlot> AddAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default);
    Task UpdateAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlot>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default);
}
