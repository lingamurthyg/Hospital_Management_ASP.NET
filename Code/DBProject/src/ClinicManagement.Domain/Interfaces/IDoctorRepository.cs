using ClinicManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Doctor entity
    /// </summary>
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor?> GetByEmailAsync(string email);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<IEnumerable<Doctor>> GetByDepartmentAsync(int deptNo);
        Task<IEnumerable<Doctor>> SearchByNameAsync(string name);
        Task<Doctor> AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}
