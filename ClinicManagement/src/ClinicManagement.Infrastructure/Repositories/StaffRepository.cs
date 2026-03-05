using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<StaffRepository> _logger;

    public StaffRepository(ClinicDbContext context, ILogger<StaffRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Staff>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Staff
                .Include(s => s.User)
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

    public async Task<Staff?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Staff
                .Include(s => s.User)
                .Where(s => s.Id == id && s.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving staff with ID {StaffId}", id);
            throw;
        }
    }

    public async Task<Staff> AddAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Staff {StaffId} created successfully", staff.Id);
            return staff;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating staff");
            throw;
        }
    }

    public async Task UpdateAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Staff {StaffId} updated successfully", staff.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating staff {StaffId}", staff.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var staff = await GetByIdAsync(id, cancellationToken);
            if (staff != null)
            {
                staff.IsActive = false;
                await UpdateAsync(staff, cancellationToken);
                _logger.LogInformation("Staff {StaffId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting staff {StaffId}", id);
            throw;
        }
    }
}
