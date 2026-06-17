using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Mappings;

/// <summary>
/// AutoMapper profile for entity to DTO mappings
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Patient mappings
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)));
        CreateMap<PatientCreateDto, Patient>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));
        CreateMap<PatientUpdateDto, Patient>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Doctor mappings
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DeptName : string.Empty));
        CreateMap<DoctorCreateDto, Doctor>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.ReputationIndex, opt => opt.MapFrom(src => 0f))
            .ForMember(dest => dest.PatientsTreated, opt => opt.MapFrom(src => 0));
        CreateMap<DoctorUpdateDto, Doctor>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : string.Empty))
            .ForMember(dest => dest.TimeSlotInfo, opt => opt.MapFrom(src => src.TimeSlot != null ? $"{src.TimeSlot.StartTime} - {src.TimeSlot.EndTime}" : string.Empty));
        CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enums.AppointmentStatus.Pending))
            .ForMember(dest => dest.FeedbackGiven, opt => opt.MapFrom(src => false));
        CreateMap<AppointmentUpdateDto, Appointment>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Department mappings
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentCreateDto, Department>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        CreateMap<DepartmentUpdateDto, Department>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Staff mappings
        CreateMap<Staff, StaffDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)));
        CreateMap<StaffCreateDto, Staff>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));
        CreateMap<StaffUpdateDto, Staff>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Bill mappings
        CreateMap<Bill, BillDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty));
        CreateMap<BillCreateDto, Bill>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => false));
        CreateMap<BillUpdateDto, Bill>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // TreatmentHistory mappings
        CreateMap<TreatmentHistory, TreatmentHistoryDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty));
        CreateMap<TreatmentHistoryCreateDto, TreatmentHistory>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.TreatmentDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<TreatmentHistoryUpdateDto, TreatmentHistory>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // TimeSlot mappings
        CreateMap<TimeSlot, TimeSlotDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : string.Empty));
        CreateMap<TimeSlotCreateDto, TimeSlot>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true));
        CreateMap<TimeSlotUpdateDto, TimeSlot>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
}

/// <summary>
/// Department DTO
/// </summary>
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

public class DepartmentUpdateDto
{
    public string DeptName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

/// <summary>
/// Staff DTOs
/// </summary>
public class StaffDto
{
    public int StaffID { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Qualification { get; set; } = string.Empty;
    public int Status { get; set; }
}

public class StaffCreateDto
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

public class StaffUpdateDto
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

/// <summary>
/// Bill DTOs
/// </summary>
public class BillDto
{
    public int BillID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public bool IsPaid { get; set; }
    public string? Description { get; set; }
}

public class BillCreateDto
{
    public int PatientID { get; set; }
    public int AppointmentID { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}

public class BillUpdateDto
{
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public string? Description { get; set; }
}

/// <summary>
/// TreatmentHistory DTOs
/// </summary>
public class TreatmentHistoryDto
{
    public int TreatmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorID { get; set; }
    public DateTime TreatmentDate { get; set; }
    public string Disease { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Progress { get; set; }
}

public class TreatmentHistoryCreateDto
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public string Disease { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Progress { get; set; }
}

public class TreatmentHistoryUpdateDto
{
    public string Disease { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Progress { get; set; }
}

/// <summary>
/// TimeSlot DTOs
/// </summary>
public class TimeSlotDto
{
    public int TimeSlotID { get; set; }
    public int DoctorID { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime SlotDate { get; set; }
}

public class TimeSlotCreateDto
{
    public int DoctorID { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime SlotDate { get; set; }
}

public class TimeSlotUpdateDto
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime SlotDate { get; set; }
}
