using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<DoctorRepository> _logger;

    public DoctorRepository(ClinicDbContext context, ILogger<DoctorRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all doctors");
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.IsActive && d.Status == 1)
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
            _logger.LogInformation("Retrieving doctor with ID: {DoctorId}", id);
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DoctorID == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new doctor: {DoctorName}", doctor.Name);
            doctor.CreatedDate = DateTime.UtcNow;
            doctor.IsActive = true;
            doctor.Status = 1;
            doctor.ReputationIndex = 0;
            doctor.PatientsTreated = 0;
            
            await _context.Doctors.AddAsync(doctor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully added doctor with ID: {DoctorId}", doctor.DoctorID);
            return doctor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding doctor: {DoctorName}", doctor.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating doctor with ID: {DoctorId}", doctor.DoctorID);
            doctor.ModifiedDate = DateTime.UtcNow;
            
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully updated doctor with ID: {DoctorId}", doctor.DoctorID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor with ID: {DoctorId}", doctor.DoctorID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting doctor with ID: {DoctorId}", id);
            var doctor = await _context.Doctors.FindAsync(new object[] { id }, cancellationToken);
            
            if (doctor != null)
            {
                doctor.IsActive = false;
                doctor.Status = 0;
                doctor.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully deleted doctor with ID: {DoctorId}", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting doctor with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .AnyAsync(d => d.DoctorID == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if doctor exists with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching doctors with term: {SearchTerm}", searchTerm);
            
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.IsActive && d.Status == 1 && 
                       (d.Name.Contains(searchTerm) || d.Specialization.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching doctors with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving doctors for department: {DeptNo}", deptNo);
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.DeptNo == deptNo && d.IsActive && d.Status == 1)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctors for department: {DeptNo}", deptNo);
            throw;
        }
    }

    public async Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving doctor with email: {Email}", email);
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Email == email && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with email: {Email}", email);
            throw;
        }
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .AnyAsync(d => d.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if doctor email exists: {Email}", email);
            throw;
        }
    }
}
