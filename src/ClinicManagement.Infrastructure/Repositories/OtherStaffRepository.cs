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
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<OtherStaff>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaff
            .Where(s => s.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<OtherStaff?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaff
            .Where(s => s.StaffID == id && s.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OtherStaff> AddAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        await _context.OtherStaff.AddAsync(staff, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return staff;
    }

    public async Task UpdateAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        _context.OtherStaff.Update(staff);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var staff = await GetByIdAsync(id, cancellationToken);
        if (staff != null)
        {
            staff.IsActive = false;
            await UpdateAsync(staff, cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaff
            .AnyAsync(s => s.StaffID == id && s.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<OtherStaff>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaff
            .Where(s => s.IsActive &&
                (s.Name.Contains(searchTerm) ||
                 s.Email.Contains(searchTerm) ||
                 s.Role.Contains(searchTerm)))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
