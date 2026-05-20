using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Doctor entity
/// </summary>
public class DoctorRepository : IDoctorRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<DoctorRepository> _logger;

    public DoctorRepository(ClinicDbContext context, ILogger<DoctorRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.Status)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all doctors");
            throw;
        }
    }

    public async Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DoctorID == id && d.Status, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with ID {DoctorId}", id);
            throw;
        }
    }

    public async Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Email == email && d.Status, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with email {Email}", email);
            throw;
        }
    }

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Doctor {DoctorId} created successfully", doctor.DoctorID);
            return doctor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding doctor");
            throw;
        }
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Doctor {DoctorId} updated successfully", doctor.DoctorID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor {DoctorId}", doctor.DoctorID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctor = await _context.Doctors.FindAsync(new object[] { id }, cancellationToken);
            if (doctor != null)
            {
                doctor.Status = false;
                doctor.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Doctor {DoctorId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting doctor {DoctorId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors.AnyAsync(d => d.DoctorID == id && d.Status, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if doctor {DoctorId} exists", id);
            throw;
        }
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors.AnyAsync(d => d.Email == email && d.Status, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if email {Email} exists", email);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.Status && (d.Name.Contains(searchTerm) || d.Email.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching doctors with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.DeptNo == deptNo && d.Status)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctors for department {DeptNo}", deptNo);
            throw;
        }
    }

    public async Task<Doctor?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Email == email && d.Password == password && d.Status, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login for email {Email}", email);
            throw;
        }
    }
}
