using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<PatientRepository> _logger;

    public PatientRepository(ClinicDbContext context, ILogger<PatientRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all patients");
            throw;
        }
    }

    public async Task<Patient?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.Id == id && p.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient with ID {PatientId}", id);
            throw;
        }
    }

    public async Task<Patient?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.UserId == userId && p.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient with user ID {UserId}", userId);
            throw;
        }
    }

    public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Patient {PatientId} created successfully", patient.Id);
            return patient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating patient");
            throw;
        }
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Patient {PatientId} updated successfully", patient.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient {PatientId}", patient.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var patient = await GetByIdAsync(id, cancellationToken);
            if (patient != null)
            {
                patient.IsActive = false;
                await UpdateAsync(patient, cancellationToken);
                _logger.LogInformation("Patient {PatientId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient {PatientId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients.AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if patient {PatientId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.IsActive && (p.User != null && p.User.Name.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching patients with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
