using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Appointment entity
/// </summary>
public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<AppointmentRepository> _logger;

    public AppointmentRepository(ClinicDbContext context, ILogger<AppointmentRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all appointments");
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
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
            _logger.LogInformation("Retrieving appointment with ID: {AppointmentId}", id);
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AppointmentID == id && a.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointment with ID: {AppointmentId}", id);
            throw;
        }
    }

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new appointment for patient: {PatientId}", appointment.PatientID);
            appointment.CreatedDate = DateTime.UtcNow;
            appointment.IsActive = true;
            appointment.Status = AppointmentStatus.Pending;
            appointment.FeedbackGiven = false;
            
            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully added appointment with ID: {AppointmentId}", appointment.AppointmentID);
            return appointment;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding appointment for patient: {PatientId}", appointment.PatientID);
            throw;
        }
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating appointment with ID: {AppointmentId}", appointment.AppointmentID);
            appointment.ModifiedDate = DateTime.UtcNow;
            
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Successfully updated appointment with ID: {AppointmentId}", appointment.AppointmentID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment with ID: {AppointmentId}", appointment.AppointmentID);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting appointment with ID: {AppointmentId}", id);
            var appointment = await _context.Appointments.FindAsync(new object[] { id }, cancellationToken);
            
            if (appointment != null)
            {
                appointment.IsActive = false;
                appointment.Status = AppointmentStatus.Cancelled;
                appointment.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully deleted appointment with ID: {AppointmentId}", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment with ID: {AppointmentId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Appointments
                .AnyAsync(a => a.AppointmentID == id && a.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if appointment exists with ID: {AppointmentId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving appointments for patient: {PatientId}", patientId);
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
                .Where(a => a.PatientID == patientId && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointments for patient: {PatientId}", patientId);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving appointments for doctor: {DoctorId}", doctorId);
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.DoctorID == doctorId && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetPendingAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving pending appointments for doctor: {DoctorId}", doctorId);
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.DoctorID == doctorId && a.Status == AppointmentStatus.Pending && a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving today's appointments for doctor: {DoctorId}", doctorId);
            var today = DateTime.Today;
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.TimeSlot)
                .Where(a => a.DoctorID == doctorId && 
                           a.AppointmentDate.Date == today && 
                           a.Status == AppointmentStatus.Approved && 
                           a.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving today's appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<Appointment?> GetCurrentAppointmentByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving current appointment for patient: {PatientId}", patientId);
            var today = DateTime.Today;
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
                .Where(a => a.PatientID == patientId && 
                           a.AppointmentDate.Date == today && 
                           a.Status == AppointmentStatus.Approved && 
                           a.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving current appointment for patient: {PatientId}", patientId);
            throw;
        }
    }

    public async Task<Appointment?> GetPendingFeedbackByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving pending feedback appointment for patient: {PatientId}", patientId);
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.TimeSlot)
                .Where(a => a.PatientID == patientId && 
                           a.Status == AppointmentStatus.Completed && 
                           !a.FeedbackGiven && 
                           a.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending feedback appointment for patient: {PatientId}", patientId);
            throw;
        }
    }

    public async Task ApproveAppointmentAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Approving appointment with ID: {AppointmentId}", appointmentId);
            var appointment = await _context.Appointments.FindAsync(new object[] { appointmentId }, cancellationToken);
            
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Approved;
                appointment.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully approved appointment with ID: {AppointmentId}", appointmentId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error approving appointment with ID: {AppointmentId}", appointmentId);
            throw;
        }
    }
}
