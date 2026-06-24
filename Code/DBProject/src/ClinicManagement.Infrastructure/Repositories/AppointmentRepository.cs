using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Repositories
{
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

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.FreeSlot)
                .Include(a => a.Bill)
                .FirstOrDefaultAsync(a => a.AppointmentID == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.FreeSlot)
                .Where(a => a.PatientID == patientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.FreeSlot)
                .Where(a => a.DoctorID == doctorId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetPendingByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.FreeSlot)
                .Where(a => a.DoctorID == doctorId && !a.IsApproved && !a.IsCompleted)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetTodaysByDoctorIdAsync(int doctorId)
        {
            var today = DateTime.Today;
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.FreeSlot)
                .Where(a => a.DoctorID == doctorId 
                    && a.IsApproved 
                    && !a.IsCompleted
                    && a.AppointmentDate.Date == today)
                .OrderBy(a => a.Timings)
                .ToListAsync();
        }

        public async Task<Appointment?> GetCurrentAppointmentByPatientIdAsync(int patientId)
        {
            var today = DateTime.Today;
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.FreeSlot)
                .Where(a => a.PatientID == patientId 
                    && a.IsApproved 
                    && !a.IsCompleted
                    && a.AppointmentDate.Date == today)
                .FirstOrDefaultAsync();
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentID == id);
        }
    }
}
