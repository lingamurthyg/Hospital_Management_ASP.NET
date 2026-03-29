namespace ClinicManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for OtherStaff operations
/// </summary>
public interface IOtherStaffService
{
    Task<IEnumerable<OtherStaffDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OtherStaffDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OtherStaffDto> CreateAsync(OtherStaffCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, OtherStaffUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OtherStaffDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}

public class OtherStaffDto
{
    public int StaffID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class OtherStaffCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class OtherStaffUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
