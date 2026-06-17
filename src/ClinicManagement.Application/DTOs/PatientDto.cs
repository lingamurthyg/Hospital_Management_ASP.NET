using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

/// <summary>
/// Data transfer object for Patient
/// </summary>
public class PatientDto
{
    public int PatientID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new patient
/// </summary>
public class PatientCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
}

/// <summary>
/// DTO for updating a patient
/// </summary>
public class PatientUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
}

/// <summary>
/// DTO for patient login
/// </summary>
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// DTO for login response
/// </summary>
public class LoginResponseDto
{
    public bool IsValid { get; set; }
    public int UserId { get; set; }
    public int UserType { get; set; }
    public string Message { get; set; } = string.Empty;
}
