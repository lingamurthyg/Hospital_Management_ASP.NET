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
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks
            .Include(f => f.Patient)
            .Include(f => f.Doctor)
            .Where(f => f.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Feedback?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks
            .Include(f => f.Patient)
            .Include(f => f.Doctor)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id && f.IsActive, cancellationToken);
    }

    public async Task<Feedback> AddAsync(Feedback feedback, CancellationToken cancellationToken = default)
    {
        await _context.Feedbacks.AddAsync(feedback, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return feedback;
    }

    public async Task UpdateAsync(Feedback feedback, CancellationToken cancellationToken = default)
    {
        _context.Feedbacks.Update(feedback);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var feedback = await _context.Feedbacks.FindAsync(new object[] { id }, cancellationToken);
        if (feedback != null)
        {
            feedback.IsActive = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks.AnyAsync(f => f.Id == id && f.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Feedback>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks
            .Include(f => f.Doctor)
            .Where(f => f.PatientId == patientId && f.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Feedback>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks
            .Include(f => f.Patient)
            .Where(f => f.DoctorId == doctorId && f.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Feedback?> GetPendingFeedbackByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks
            .Include(f => f.Doctor)
            .Include(f => f.Appointment)
            .Where(f => f.PatientId == patientId && f.IsActive)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
