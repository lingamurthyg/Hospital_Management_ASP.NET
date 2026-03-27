using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class BillRepository : IBillRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<BillRepository> _logger;

    public BillRepository(ClinicDbContext context, ILogger<BillRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Bill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Bills.Include(b => b.Patient).Include(b => b.Appointment).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Bills.Include(b => b.Patient).Include(b => b.Appointment).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills.Where(b => b.PatientId == patientId).Include(b => b.Appointment).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills.AsNoTracking().FirstOrDefaultAsync(b => b.AppointmentId == appointmentId, cancellationToken);
    }

    public async Task<Bill> AddAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        await _context.Bills.AddAsync(bill, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return bill;
    }

    public async Task UpdateAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        _context.Bills.Update(bill);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var bill = await _context.Bills.FindAsync(new object[] { id }, cancellationToken);
        if (bill != null)
        {
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
