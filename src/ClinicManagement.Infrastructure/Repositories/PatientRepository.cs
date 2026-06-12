using Microsoft.EntityFrameworkCore;
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

    public PatientRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PatientID == id && p.IsActive, cancellationToken);
    }

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email && p.IsActive, cancellationToken);
    }

    public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        patient.CreatedDate = DateTime.UtcNow;
        patient.IsActive = true;
        
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return patient;
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        patient.ModifiedDate = DateTime.UtcNow;
        
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var patient = await _context.Patients.FindAsync(new object[] { id }, cancellationToken);
        if (patient != null)
        {
            patient.IsActive = false;
            patient.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AnyAsync(p => p.PatientID == id && p.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.IsActive && p.Name.Contains(searchTerm))
            .ToListAsync(cancellationToken);
    }

    public async Task<(string email, string password, int type, int id)?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email && p.Password == password && p.IsActive, cancellationToken);
        
        if (patient != null)
        {
            return (patient.Email, patient.Password, 1, patient.PatientID); // type 1 = patient
        }
        
        return null;
    }
}
