using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

public class OtherStaffRepository : IOtherStaffRepository
{
    private readonly ClinicDbContext _context;

    public OtherStaffRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OtherStaff>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaffs
            .AsNoTracking()
            .Where(s => s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<OtherStaff?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaffs
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.StaffID == id && s.IsActive, cancellationToken);
    }

    public async Task<OtherStaff> AddAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        staff.CreatedDate = DateTime.UtcNow;
        staff.IsActive = true;
        
        await _context.OtherStaffs.AddAsync(staff, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return staff;
    }

    public async Task UpdateAsync(OtherStaff staff, CancellationToken cancellationToken = default)
    {
        staff.ModifiedDate = DateTime.UtcNow;
        
        _context.OtherStaffs.Update(staff);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var staff = await _context.OtherStaffs.FindAsync(new object[] { id }, cancellationToken);
        if (staff != null)
        {
            staff.IsActive = false;
            staff.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaffs
            .AnyAsync(s => s.StaffID == id && s.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<OtherStaff>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.OtherStaffs
            .AsNoTracking()
            .Where(s => s.IsActive && s.Name.Contains(searchTerm))
            .ToListAsync(cancellationToken);
    }
}
