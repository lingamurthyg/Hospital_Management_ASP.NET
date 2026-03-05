namespace ClinicManagement.Application.DTOs;

public class StaffDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Position { get; set; } = string.Empty;
    public string? Department { get; set; }
    public string? UserName { get; set; }
}

public class StaffCreateDto
{
    public int UserId { get; set; }
    public string Position { get; set; } = string.Empty;
    public string? Department { get; set; }
}

public class StaffUpdateDto
{
    public string Position { get; set; } = string.Empty;
    public string? Department { get; set; }
}
