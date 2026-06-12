using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Appointment entity
/// </summary>
public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicDbContext _context;

    public AppointmentRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync(cancellationToken);
    }

    public async Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.AppointmentID == id, cancellationToken);
    }

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        appointment.CreatedDate = DateTime.UtcNow;
        
        await _context.Appointments.AddAsync(appointment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return appointment;
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        appointment.ModifiedDate = DateTime.UtcNow;
        
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var appointment = await _context.Appointments.FindAsync(new object[] { id }, cancellationToken);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AnyAsync(a => a.AppointmentID == id, cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetPendingAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorID == doctorId && a.Status == "Pending")
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorID == doctorId && 
                       a.AppointmentDate.Date == today &&
                       a.Status == "Approved")
            .ToListAsync(cancellationToken);
    }

    public async Task<Appointment?> GetCurrentAppointmentByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.PatientID == patientId && 
                                     a.AppointmentDate.Date == today &&
                                     a.Status == "Approved", cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Doctor)
            .Where(a => a.PatientID == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }

    public async Task ApproveAppointmentAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = await _context.Appointments.FindAsync(new object[] { appointmentId }, cancellationToken);
        if (appointment != null)
        {
            appointment.Status = "Approved";
            appointment.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
