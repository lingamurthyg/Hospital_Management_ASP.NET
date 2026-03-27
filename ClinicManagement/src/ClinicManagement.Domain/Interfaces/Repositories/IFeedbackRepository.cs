using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Interfaces.Repositories;

public interface IFeedbackRepository
{
    Task<IEnumerable<Feedback>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Feedback?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Feedback>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<Feedback?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default);
    Task<Feedback> AddAsync(Feedback feedback, CancellationToken cancellationToken = default);
    Task UpdateAsync(Feedback feedback, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
