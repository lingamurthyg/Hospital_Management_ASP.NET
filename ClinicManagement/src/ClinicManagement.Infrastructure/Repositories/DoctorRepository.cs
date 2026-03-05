using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

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
                .Include(d => d.User)
                .Where(d => d.IsActive)
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
                .Include(d => d.User)
                .Where(d => d.Id == id && d.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with ID {DoctorId}", id);
            throw;
        }
    }

    public async Task<Doctor?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.UserId == userId && d.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with user ID {UserId}", userId);
            throw;
        }
    }

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Doctor {DoctorId} created successfully", doctor.Id);
            return doctor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating doctor");
            throw;
        }
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Doctor {DoctorId} updated successfully", doctor.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor {DoctorId}", doctor.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctor = await GetByIdAsync(id, cancellationToken);
            if (doctor != null)
            {
                doctor.IsActive = false;
                await UpdateAsync(doctor, cancellationToken);
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
            return await _context.Doctors.AnyAsync(d => d.Id == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if doctor {DoctorId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.IsActive &&
                    ((d.User != null && d.User.Name.Contains(searchTerm)) ||
                     d.Specialization.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching doctors with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.IsActive && d.Specialization == specialization)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctors with specialization {Specialization}", specialization);
            throw;
        }
    }
}
