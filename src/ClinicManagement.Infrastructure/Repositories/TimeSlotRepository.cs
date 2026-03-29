using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class TimeSlotRepository : ITimeSlotRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<TimeSlotRepository> _logger;

    public TimeSlotRepository(ClinicDbContext context, ILogger<TimeSlotRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<TimeSlot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .Include(ts => ts.Doctor)
            .Where(ts => ts.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TimeSlot?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .Include(ts => ts.Doctor)
            .Where(ts => ts.TimeSlotID == id && ts.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TimeSlot> AddAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default)
    {
        await _context.TimeSlots.AddAsync(timeSlot, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return timeSlot;
    }

    public async Task UpdateAsync(TimeSlot timeSlot, CancellationToken cancellationToken = default)
    {
        _context.TimeSlots.Update(timeSlot);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var timeSlot = await GetByIdAsync(id, cancellationToken);
        if (timeSlot != null)
        {
            timeSlot.IsActive = false;
            await UpdateAsync(timeSlot, cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .AnyAsync(ts => ts.TimeSlotID == id && ts.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<TimeSlot>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .Include(ts => ts.Doctor)
            .Where(ts => ts.IsActive && ts.Doctor != null && ts.Doctor.Name.Contains(searchTerm))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .Include(ts => ts.Doctor)
            .Where(ts => ts.DoctorID == doctorId && ts.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots
            .Include(ts => ts.Doctor)
            .Where(ts => ts.DoctorID == doctorId &&
                         ts.SlotDate.Date == date.Date &&
                         ts.IsAvailable &&
                         ts.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
