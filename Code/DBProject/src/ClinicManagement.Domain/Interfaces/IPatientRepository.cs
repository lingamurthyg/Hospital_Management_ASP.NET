using ClinicManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Patient entity
    /// </summary>
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient?> GetByEmailAsync(string email);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<IEnumerable<Patient>> SearchByNameAsync(string name);
        Task<Patient> AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}
