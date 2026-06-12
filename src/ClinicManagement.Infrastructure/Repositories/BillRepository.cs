using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

public class BillRepository : IBillRepository
{
    private readonly ClinicDbContext _context;

    public BillRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .FirstOrDefaultAsync(b => b.BillID == id, cancellationToken);
    }

    public async Task<Bill> AddAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        bill.CreatedDate = DateTime.UtcNow;
        
        await _context.Bills.AddAsync(bill, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return bill;
    }

    public async Task UpdateAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        bill.ModifiedDate = DateTime.UtcNow;
        
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

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AnyAsync(b => b.BillID == id, cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetBillsByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AsNoTracking()
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetBillsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Bills
            .AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Appointment)
            .Where(b => b.DoctorID == doctorId)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync(cancellationToken);
    }
}
