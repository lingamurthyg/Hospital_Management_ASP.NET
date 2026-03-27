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
        return await _context.Staff.Where(s => s.IsActive).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Staff?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Staff.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
    }

    public async Task<Staff> AddAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        await _context.Staff.AddAsync(staff, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return staff;
    }

    public async Task UpdateAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        _context.Staff.Update(staff);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var staff = await _context.Staff.FindAsync(new object[] { id }, cancellationToken);
        if (staff != null)
        {
            staff.IsActive = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<Staff>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.Staff.Where(s => s.IsActive && s.Name.Contains(searchTerm)).AsNoTracking().ToListAsync(cancellationToken);
    }
}
