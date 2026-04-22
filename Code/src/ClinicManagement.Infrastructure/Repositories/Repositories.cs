using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ClinicDbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ILogger _logger;

    public Repository(ClinicDbContext context, ILogger logger)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null;
    }
}

/// <summary>
/// Patient repository implementation
/// </summary>
public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ClinicDbContext context, ILogger<PatientRepository> logger) : base(context, logger)
    {
    }

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email && p.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.Name.Contains(name) && p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.Email == email, cancellationToken);
    }

    public override async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Doctor repository implementation
/// </summary>
public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ClinicDbContext context, ILogger<DoctorRepository> logger) : base(context, logger)
    {
    }

    public async Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.Email == email && d.Status, cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.DeptNo == deptNo && d.Status)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.Name.Contains(name) && d.Status)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(d => d.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(d => d.Department)
            .Where(d => d.Status)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Department repository implementation
/// </summary>
public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ClinicDbContext context, ILogger<DepartmentRepository> logger) : base(context, logger)
    {
    }

    public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeptName == name && d.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Department>> GetActiveDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(d => d.Doctors)
            .Where(d => d.IsActive)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Appointment repository implementation
/// </summary>
public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ClinicDbContext context, ILogger<AppointmentRepository> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientID == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorID == doctorId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetPendingAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorID == doctorId && a.Status == Domain.Enums.AppointmentStatus.Pending)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorID == doctorId && 
                   a.AppointmentDate.Date == today &&
                   a.Status == Domain.Enums.AppointmentStatus.Approved)
            .OrderBy(a => a.Timings)
            .ToListAsync(cancellationToken);
    }

    public async Task<Appointment?> GetCurrentAppointmentByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.PatientID == patientId && 
                                a.AppointmentDate.Date == today &&
                                a.Status == Domain.Enums.AppointmentStatus.Approved, 
                                cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentHistoryByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientID == patientId && 
                   a.Status == Domain.Enums.AppointmentStatus.Completed)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Slot repository implementation
/// </summary>
public class SlotRepository : Repository<Slot>, ISlotRepository
{
    public SlotRepository(ClinicDbContext context, ILogger<SlotRepository> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Slot>> GetAvailableSlotsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(s => s.DoctorID == doctorId && s.IsAvailable && s.SlotDate >= DateTime.Today)
            .OrderBy(s => s.SlotDate)
            .ThenBy(s => s.Timings)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Slot>> GetSlotsByDoctorAndDateAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(s => s.DoctorID == doctorId && s.SlotDate.Date == date.Date)
            .OrderBy(s => s.Timings)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Bill repository implementation
/// </summary>
public class BillRepository : Repository<Bill>, IBillRepository
{
    public BillRepository(ClinicDbContext context, ILogger<BillRepository> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .Where(b => b.DoctorID == doctorId)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .FirstOrDefaultAsync(b => b.AppointmentID == appointmentId, cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetUnpaidBillsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(b => b.Patient)
            .Include(b => b.Doctor)
            .Include(b => b.Appointment)
            .Where(b => b.PaymentStatus == Domain.Enums.PaymentStatus.Unpaid)
            .OrderBy(b => b.BillDate)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Feedback repository implementation
/// </summary>
public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(ClinicDbContext context, ILogger<FeedbackRepository> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Feedback>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(f => f.Patient)
            .Include(f => f.Doctor)
            .Where(f => f.PatientID == patientId)
            .OrderByDescending(f => f.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Feedback>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Include(f => f.Patient)
            .Include(f => f.Doctor)
            .Where(f => f.DoctorID == doctorId)
            .OrderByDescending(f => f.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Feedback?> GetPendingFeedbackByPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        // This would need additional logic to determine pending feedback
        // For now, return null
        return null;
    }
}

/// <summary>
/// Staff repository implementation
/// </summary>
public class StaffRepository : Repository<Staff>, IStaffRepository
{
    public StaffRepository(ClinicDbContext context, ILogger<StaffRepository> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Staff>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(s => s.Name.Contains(name) && s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Staff>> GetActiveStaffAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(s => s.IsActive)
            .ToListAsync(cancellationToken);
    }
}

/// <summary>
/// Admin repository implementation
/// </summary>
public class AdminRepository : Repository<Admin>, IAdminRepository
{
    public AdminRepository(ClinicDbContext context, ILogger<AdminRepository> logger) : base(context, logger)
    {
    }

    public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Email == email && a.IsActive, cancellationToken);
    }
}
