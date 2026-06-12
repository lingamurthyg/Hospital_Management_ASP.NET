using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Doctor entity
/// </summary>
public class DoctorRepository : IDoctorRepository
{
    private readonly ClinicDbContext _context;

    public DoctorRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.Status)
            .ToListAsync(cancellationToken);
    }

    public async Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.DoctorID == id && d.Status, cancellationToken);
    }

    public async Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Email == email && d.Status, cancellationToken);
    }

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        doctor.CreatedDate = DateTime.UtcNow;
        doctor.Status = true;
        
        await _context.Doctors.AddAsync(doctor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return doctor;
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        doctor.ModifiedDate = DateTime.UtcNow;
        
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await _context.Doctors.FindAsync(new object[] { id }, cancellationToken);
        if (doctor != null)
        {
            doctor.Status = false;
            doctor.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AnyAsync(d => d.DoctorID == id && d.Status, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AnyAsync(d => d.Email == email && d.Status, cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.Status && d.Name.Contains(searchTerm))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.DeptNo == deptNo && d.Status)
            .ToListAsync(cancellationToken);
    }
}
