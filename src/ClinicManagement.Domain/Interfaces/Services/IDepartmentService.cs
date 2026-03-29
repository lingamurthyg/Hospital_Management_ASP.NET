namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Department operations
/// </summary>
public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, DepartmentUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DepartmentDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}

public class DepartmentDto
{
    public int DepartmentID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class DepartmentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class DepartmentUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
