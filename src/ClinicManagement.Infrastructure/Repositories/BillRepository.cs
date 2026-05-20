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
        try
        {
            return await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all bills");
            throw;
        }
    }

    public async Task<Bill?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BillID == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bill with ID {BillId}", id);
            throw;
        }
    }

    public async Task<Bill> AddAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Bill {BillId} created successfully", bill.BillID);
            return bill;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding bill");
            throw;
        }
    }

    public async Task UpdateAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Bill {BillId} updated successfully", bill.BillID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating bill {BillId}", bill.BillID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var bill = await _context.Bills.FindAsync(new object[] { id }, cancellationToken);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Bill {BillId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting bill {BillId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Bills.AnyAsync(b => b.BillID == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if bill {BillId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .Where(b => b.PatientID == patientId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bills for patient {PatientId}", patientId);
            throw;
        }
    }

    public async Task<IEnumerable<Bill>> GetUnpaidBillsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .Where(b => b.Appointment!.DoctorID == doctorId && !b.IsPaid)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving unpaid bills for doctor {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task MarkAsPaidAsync(int billId, CancellationToken cancellationToken = default)
    {
        try
        {
            var bill = await _context.Bills.FindAsync(new object[] { billId }, cancellationToken);
            if (bill != null)
            {
                bill.IsPaid = true;
                bill.PaidDate = DateTime.UtcNow;
                bill.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Bill {BillId} marked as paid", billId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking bill {BillId} as paid", billId);
            throw;
        }
    }
}
