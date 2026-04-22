using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.DTOs;

/// <summary>
/// Patient DTOs
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
}

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

public class PatientUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

/// <summary>
/// Doctor DTOs
/// </summary>
public class DoctorDto
{
    public int DoctorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int DeptNo { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Qualification { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public decimal ReputationIndex { get; set; }
    public int PatientsTreated { get; set; }
    public int Age { get; set; }
}

public class DoctorCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int DeptNo { get; set; }
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Qualification { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}

public class DoctorUpdateDto
{
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal ChargesPerVisit { get; set; }
}

/// <summary>
/// Department DTOs
/// </summary>
public class DepartmentDto
{
    public int DeptNo { get; set; }
    public string DeptName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DoctorCount { get; set; }
}

public class DepartmentCreateDto
{
    public string DeptName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Appointment DTOs
/// </summary>
public class AppointmentDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Timings { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; }
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
}

public class AppointmentCreateDto
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int SlotID { get; set; }
}

public class AppointmentUpdateDto
{
    public string? Disease { get; set; }
    public string? Progress { get; set; }
    public string? Prescription { get; set; }
    public AppointmentStatus Status { get; set; }
}

/// <summary>
/// Slot DTOs
/// </summary>
public class SlotDto
{
    public int SlotID { get; set; }
    public int DoctorID { get; set; }
    public DateTime SlotDate { get; set; }
    public string Timings { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}

/// <summary>
/// Bill DTOs
/// </summary>
public class BillDto
{
    public int BillID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime BillDate { get; set; }
}

public class BillCreateDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public decimal Amount { get; set; }
}

/// <summary>
/// Feedback DTOs
/// </summary>
public class FeedbackDto
{
    public int FeedbackID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class FeedbackCreateDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
}

/// <summary>
/// Staff DTOs
/// </summary>
public class StaffDto
{
    public int StaffID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Designation { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}

public class StaffCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Designation { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}

/// <summary>
/// Login DTOs
/// </summary>
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResultDto
{
    public bool Success { get; set; }
    public int UserId { get; set; }
    public UserType UserType { get; set; }
    public string Message { get; set; } = string.Empty;
}
