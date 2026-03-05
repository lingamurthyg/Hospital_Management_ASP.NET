using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<FeedbackRepository> _logger;

    public FeedbackRepository(ClinicDbContext context, ILogger<FeedbackRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Feedbacks
                .Include(f => f.Patient).ThenInclude(p => p!.User)
                .Where(f => f.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all feedbacks");
            throw;
        }
    }

    public async Task<Feedback?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Feedbacks
                .Include(f => f.Patient).ThenInclude(p => p!.User)
                .Where(f => f.Id == id && f.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving feedback with ID {FeedbackId}", id);
            throw;
        }
    }

    public async Task<Feedback> AddAsync(Feedback feedback, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Feedback {FeedbackId} created successfully", feedback.Id);
            return feedback;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating feedback");
            throw;
        }
    }

    public async Task UpdateAsync(Feedback feedback, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Feedback {FeedbackId} updated successfully", feedback.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating feedback {FeedbackId}", feedback.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var feedback = await GetByIdAsync(id, cancellationToken);
            if (feedback != null)
            {
                feedback.IsActive = false;
                await UpdateAsync(feedback, cancellationToken);
                _logger.LogInformation("Feedback {FeedbackId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting feedback {FeedbackId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Feedback>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Feedbacks
                .Include(f => f.Patient).ThenInclude(p => p!.User)
                .Where(f => f.PatientId == patientId && f.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving feedbacks for patient {PatientId}", patientId);
            throw;
        }
    }
}
