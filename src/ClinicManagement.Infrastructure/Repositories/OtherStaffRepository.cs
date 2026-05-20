using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class OtherStaffRepository : IOtherStaffRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<OtherStaffRepository> _logger;

    public OtherStaffRepository(ClinicDbContext context, ILogger<OtherStaffRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<OtherStaff>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.OtherStaffs
                .Where(s => s.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all staff");
            throw;
        }
    }

    public async Task<OtherStaff?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.OtherStaffs
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StaffID == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving staff with ID {StaffId}", id);
            throw;
        }
    }

    public async Task<OtherStaff> AddAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.OtherStaffs.Add(staff);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Staff {StaffId} created successfully", staff.StaffID);
            return staff;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding staff");
            throw;
        }
    }

    public async Task UpdateAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.OtherStaffs.Update(staff);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Staff {StaffId} updated successfully", staff.StaffID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating staff {StaffId}", staff.StaffID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var staff = await _context.OtherStaffs.FindAsync(new object[] { id }, cancellationToken);
            if (staff != null)
            {
                staff.IsActive = false;
                staff.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Staff {StaffId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting staff {StaffId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.OtherStaffs.AnyAsync(s => s.StaffID == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if staff {StaffId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<OtherStaff>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.OtherStaffs
                .Where(s => s.IsActive && s.Name.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching staff with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
