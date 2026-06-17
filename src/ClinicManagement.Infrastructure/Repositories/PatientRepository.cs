using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Patient entity
/// </summary>
public class PatientRepository : IPatientRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<PatientRepository> _logger;

    public PatientRepository(ClinicDbContext context, ILogger<PatientRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all patients");
            return await _context.Patients
                .Where(p => p.IsActive && p.Status == 1)
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
            _logger.LogInformation("Retrieving patient with ID: {PatientId}", id);
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientID == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient with ID: {PatientId}", id);
            throw;
        }
    }

    public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new patient: {PatientName}", patient.Name);
            patient.CreatedDate = DateTime.UtcNow;
            patient.IsActive = true;
            patient.Status = 1;
            
            await _context.Patients.AddAsync(patient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully added patient with ID: {PatientId}", patient.PatientID);
            return patient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding patient: {PatientName}", patient.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating patient with ID: {PatientId}", patient.PatientID);
            patient.ModifiedDate = DateTime.UtcNow;
            
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully updated patient with ID: {PatientId}", patient.PatientID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient with ID: {PatientId}", patient.PatientID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting patient with ID: {PatientId}", id);
            var patient = await _context.Patients.FindAsync(new object[] { id }, cancellationToken);
            
            if (patient != null)
            {
                patient.IsActive = false;
                patient.Status = 0;
                patient.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully deleted patient with ID: {PatientId}", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient with ID: {PatientId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Patients
                .AnyAsync(p => p.PatientID == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if patient exists with ID: {PatientId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching patients with term: {SearchTerm}", searchTerm);
            
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            return await _context.Patients
                .Where(p => p.IsActive && p.Status == 1 && 
                       (p.Name.Contains(searchTerm) || p.Email.Contains(searchTerm) || p.Phone.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching patients with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving patient with email: {Email}", email);
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient with email: {Email}", email);
            throw;
        }
    }

    public async Task<Patient?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Validating login for email: {Email}", email);
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email && p.Password == password && p.IsActive && p.Status == 1, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login for email: {Email}", email);
            throw;
        }
    }
}
