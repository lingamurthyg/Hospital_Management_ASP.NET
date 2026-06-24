using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Doctor entity
    /// </summary>
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicDbContext _context;

        public DoctorRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Appointments)
                .Include(d => d.FreeSlots)
                .FirstOrDefaultAsync(d => d.DoctorID == id);
        }

        public async Task<Doctor?> GetByEmailAsync(string email)
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.Email == email);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo)
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.DeptNo == deptNo && d.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> SearchByNameAsync(string name)
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.Name.Contains(name) && d.Status)
                .ToListAsync();
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.Status = false; // Soft delete
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.DoctorID == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Doctors.AnyAsync(d => d.Email == email);
        }
    }
}
