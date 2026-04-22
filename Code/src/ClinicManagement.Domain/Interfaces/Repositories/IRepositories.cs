using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Interfaces.Repositories;

/// <summary>
/// Generic repository interface
/// </summary>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Patient repository interface
/// </summary>
public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}

/// <summary>
/// Doctor repository interface
/// </summary>
public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Department repository interface
/// </summary>
public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Department>> GetActiveDepartmentsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Appointment repository interface
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetPendingAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetTodaysAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<Appointment?> GetCurrentAppointmentByPatientAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetAppointmentHistoryByPatientAsync(int patientId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Slot repository interface
/// </summary>
public interface ISlotRepository : IRepository<Slot>
{
    Task<IEnumerable<Slot>> GetAvailableSlotsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Slot>> GetSlotsByDoctorAndDateAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default);
}

/// <summary>
/// Bill repository interface
/// </summary>
public interface IBillRepository : IRepository<Bill>
{
    Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Bill>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<Bill?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Bill>> GetUnpaidBillsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Feedback repository interface
/// </summary>
public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Feedback>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<Feedback?> GetPendingFeedbackByPatientAsync(int patientId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Staff repository interface
/// </summary>
public interface IStaffRepository : IRepository<Staff>
{
    Task<IEnumerable<Staff>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Staff>> GetActiveStaffAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Admin repository interface
/// </summary>
public interface IAdminRepository : IRepository<Admin>
{
    Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
