namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Doctor operations
/// </summary>
public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DoctorDto> CreateAsync(DoctorCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, DoctorUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorDto>> GetByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
}

public class DoctorDto
{
    public int DoctorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? DepartmentID { get; set; }
    public string? DepartmentName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class DoctorCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? DepartmentID { get; set; }
}

public class DoctorUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? DepartmentID { get; set; }
}
