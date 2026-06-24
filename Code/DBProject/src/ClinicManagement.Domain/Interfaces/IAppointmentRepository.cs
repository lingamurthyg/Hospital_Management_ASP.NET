using ClinicManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Appointment entity
    /// </summary>
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetPendingByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetTodaysByDoctorIdAsync(int doctorId);
        Task<Appointment?> GetCurrentAppointmentByPatientIdAsync(int patientId);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
