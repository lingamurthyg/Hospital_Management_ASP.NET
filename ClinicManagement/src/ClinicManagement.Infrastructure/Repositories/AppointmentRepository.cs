using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<AppointmentRepository> _logger;

    public AppointmentRepository(ClinicDbContext context, ILogger<AppointmentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .Where(a => a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all appointments");
            throw;
        }
    }

    public async Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .Where(a => a.Id == id && a.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId}", id);
            throw;
        }
    }

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Appointment {AppointmentId} created successfully", appointment.Id);
            return appointment;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating appointment");
            throw;
        }
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Appointment {AppointmentId} updated successfully", appointment.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment {AppointmentId}", appointment.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await GetByIdAsync(id, cancellationToken);
            if (appointment != null)
            {
                appointment.IsActive = false;
                await UpdateAsync(appointment, cancellationToken);
                _logger.LogInformation("Appointment {AppointmentId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment {AppointmentId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments.AnyAsync(a => a.Id == id && a.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if appointment {AppointmentId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .Where(a => a.PatientId == patientId && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointments for patient {PatientId}", patientId);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .Where(a => a.DoctorId == doctorId && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointments for doctor {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetPendingAppointmentsAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .Where(a => a.DoctorId == doctorId && a.Status == "Pending" && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending appointments for doctor {DoctorId}", doctorId);
            throw;
        }
    }
}
