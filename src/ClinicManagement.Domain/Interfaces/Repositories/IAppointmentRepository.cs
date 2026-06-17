using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for Appointment entity
/// </summary>
public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetPendingAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetTodaysAppointmentsByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<Appointment?> GetCurrentAppointmentByPatientAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetTreatmentHistoryByPatientAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetPreviousHistoryByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task ApproveAppointmentAsync(int appointmentId, CancellationToken cancellationToken = default);
    Task UpdatePrescriptionAsync(int appointmentId, string disease, string progress, string prescription, CancellationToken cancellationToken = default);
}
