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
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Bill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.BillID == id && b.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
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
        var bill = await GetByIdAsync(id, cancellationToken);
        if (bill != null)
        {
            bill.IsActive = false;
            await UpdateAsync(bill, cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AnyAsync(b => b.BillID == id && b.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Bill>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.IsActive &&
                (b.Status.Contains(searchTerm) ||
                 (b.Description != null && b.Description.Contains(searchTerm))))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.PatientID == patientId && b.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.AppointmentID == appointmentId && b.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
