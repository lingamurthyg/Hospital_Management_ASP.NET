using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Mappings;

/// <summary>
/// AutoMapper profile for mapping between entities and DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Patient mappings
        CreateMap<Patient, PatientDto>();
        CreateMap<PatientCreateDto, Patient>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
        CreateMap<PatientUpdateDto, Patient>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());

        // Doctor mappings
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DeptName : string.Empty));
        CreateMap<DoctorCreateDto, Doctor>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ReputationIndex, opt => opt.MapFrom(src => 0f))
            .ForMember(dest => dest.PatientsTreated, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.DoctorID, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
        CreateMap<DoctorUpdateDto, Doctor>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DoctorID, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : string.Empty))
            .ForMember(dest => dest.Timings, opt => opt.MapFrom(src => src.TimeSlot != null ? src.TimeSlot.Timings : string.Empty));
        CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
            .ForMember(dest => dest.FeedbackGiven, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.AppointmentID, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
        CreateMap<AppointmentUpdateDto, Appointment>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.AppointmentID, opt => opt.Ignore())
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

        // Department mappings
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentCreateDto, Department>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.DeptNo, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());

        // Bill mappings
        CreateMap<Bill, BillDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty));
        CreateMap<BillCreateDto, Bill>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.BillID, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());

        // OtherStaff mappings
        CreateMap<OtherStaff, OtherStaffDto>();
        CreateMap<OtherStaffCreateDto, OtherStaff>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.StaffID, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
    }
}

public class DepartmentDto
{
    public int DeptNo { get; set; }
    public string DeptName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class DepartmentCreateDto
{
    public string DeptName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class BillDto
{
    public int BillID { get; set; }
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime BillDate { get; set; }
    public DateTime? PaidDate { get; set; }
}

public class BillCreateDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public decimal Amount { get; set; }
}

public class OtherStaffDto
{
    public int StaffID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Qualification { get; set; } = string.Empty;
}

public class OtherStaffCreateDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Qualification { get; set; } = string.Empty;
}
