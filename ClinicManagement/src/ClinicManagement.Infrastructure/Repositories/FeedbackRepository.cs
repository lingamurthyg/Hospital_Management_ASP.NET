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
        return await _context.Feedbacks.Include(f => f.Patient).Include(f => f.Appointment).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Feedback?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks.Include(f => f.Patient).Include(f => f.Appointment).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Feedback>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks.Where(f => f.DoctorId == doctorId).Include(f => f.Patient).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Feedback?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Feedbacks.AsNoTracking().FirstOrDefaultAsync(f => f.AppointmentId == appointmentId, cancellationToken);
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
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
