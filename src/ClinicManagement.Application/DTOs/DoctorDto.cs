namespace ClinicManagement.Application.DTOs;

/// <summary>
/// Doctor DTO for data transfer
/// </summary>
public class DoctorDto
{
    public int DoctorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public int DeptNo { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public float ReputationIndex { get; set; }
    public int PatientsTreated { get; set; }
    public int Status { get; set; }
}

/// <summary>
/// Doctor create DTO
/// </summary>
public class DoctorCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int DeptNo { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
}

/// <summary>
/// Doctor update DTO
/// </summary>
public class DoctorUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int DeptNo { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
    public decimal ChargesPerVisit { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
}
