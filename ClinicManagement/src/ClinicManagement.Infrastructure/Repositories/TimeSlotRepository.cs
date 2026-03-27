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
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TimeSlot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TimeSlot?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots.Where(t => t.DoctorId == doctorId).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimeSlot>> GetAvailableByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.TimeSlots.Where(t => t.DoctorId == doctorId && t.IsAvailable).AsNoTracking().ToListAsync(cancellationToken);
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
        var timeSlot = await _context.TimeSlots.FindAsync(new object[] { id }, cancellationToken);
        if (timeSlot != null)
        {
            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
