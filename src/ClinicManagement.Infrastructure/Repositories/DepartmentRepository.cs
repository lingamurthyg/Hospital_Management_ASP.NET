using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<DepartmentRepository> _logger;

    public DepartmentRepository(ClinicDbContext context, ILogger<DepartmentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Departments
                .Where(d => d.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all departments");
            throw;
        }
    }

    public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeptNo == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving department with ID {DeptNo}", id);
            throw;
        }
    }

    public async Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Department {DeptNo} created successfully", department.DeptNo);
            return department;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding department");
            throw;
        }
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Department {DeptNo} updated successfully", department.DeptNo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating department {DeptNo}", department.DeptNo);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var department = await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
            if (department != null)
            {
                department.IsActive = false;
                department.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Department {DeptNo} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting department {DeptNo}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Departments.AnyAsync(d => d.DeptNo == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if department {DeptNo} exists", id);
            throw;
        }
    }
}
