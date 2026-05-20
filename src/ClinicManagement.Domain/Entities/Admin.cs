namespace ClinicManagement.Domain.Entities;

/// <summary>
/// Represents an admin user in the system
/// </summary>
public class Admin
{
    public int AdminID { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}
