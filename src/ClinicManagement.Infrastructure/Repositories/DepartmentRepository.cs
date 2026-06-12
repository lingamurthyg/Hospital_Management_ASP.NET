using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ClinicDbContext _context;

    public DepartmentRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Where(d => d.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeptNo == id && d.IsActive, cancellationToken);
    }

    public async Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        department.CreatedDate = DateTime.UtcNow;
        department.IsActive = true;
        
        await _context.Departments.AddAsync(department, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return department;
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
    {
        department.ModifiedDate = DateTime.UtcNow;
        
        _context.Departments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var department = await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
        if (department != null)
        {
            department.IsActive = false;
            department.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AnyAsync(d => d.DeptNo == id && d.IsActive, cancellationToken);
    }
}
