using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Interfaces;

public interface IFeedbackService
{
    Task<IEnumerable<FeedbackDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FeedbackDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
