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
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Bills, opt => opt.Ignore())
            .ForMember(dest => dest.Feedbacks, opt => opt.Ignore());
        
        CreateMap<PatientUpdateDto, Patient>()
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)))
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Bills, opt => opt.Ignore())
            .ForMember(dest => dest.Feedbacks, opt => opt.Ignore());

        // Doctor mappings
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DeptName : string.Empty));
        
        CreateMap<DoctorCreateDto, Doctor>()
            .ForMember(dest => dest.DoctorID, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.BirthDate)))
            .ForMember(dest => dest.ReputationIndex, opt => opt.MapFrom(src => 0f))
            .ForMember(dest => dest.PatientsTreated, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlots, opt => opt.Ignore());
        
        CreateMap<DoctorUpdateDto, Doctor>()
            .ForMember(dest => dest.DoctorID, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.BirthDate, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.ReputationIndex, opt => opt.Ignore())
            .ForMember(dest => dest.PatientsTreated, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlots, opt => opt.Ignore());

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : string.Empty))
            .ForMember(dest => dest.TimeSlot, opt => opt.MapFrom(src => src.TimeSlot != null ? $"{src.TimeSlot.StartTime} - {src.TimeSlot.EndTime}" : string.Empty));
        
        CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.AppointmentID, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enums.AppointmentStatus.Pending))
            .ForMember(dest => dest.Disease, opt => opt.Ignore())
            .ForMember(dest => dest.Progress, opt => opt.Ignore())
            .ForMember(dest => dest.Prescription, opt => opt.Ignore())
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.FeedbackGiven, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlot, opt => opt.Ignore())
            .ForMember(dest => dest.Bill, opt => opt.Ignore())
            .ForMember(dest => dest.Feedback, opt => opt.Ignore());
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
}
