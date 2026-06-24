using System;

namespace ClinicManagement.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Doctor
    /// </summary>
    public class DoctorDto
    {
        public int DoctorID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int ChargesPerVisit { get; set; }
        public float ReputationIndex { get; set; }
        public int PatientsTreated { get; set; }
        public string Qualification { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int Age { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for doctor registration
    /// </summary>
    public class DoctorRegistrationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int DeptNo { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int Salary { get; set; }
        public int ChargesPerVisit { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
    }
}
