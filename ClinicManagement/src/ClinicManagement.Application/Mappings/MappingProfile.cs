using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : null));
        CreateMap<PatientCreateDto, Patient>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<PatientUpdateDto, Patient>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : null));
        CreateMap<DoctorCreateDto, Doctor>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<DoctorUpdateDto, Doctor>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null && src.Patient.User != null ? src.Patient.User.Name : null))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null && src.Doctor.User != null ? src.Doctor.User.Name : null));
        CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<AppointmentUpdateDto, Appointment>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Bill, BillDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null && src.Patient.User != null ? src.Patient.User.Name : null));
        CreateMap<BillCreateDto, Bill>()
            .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<BillUpdateDto, Bill>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Feedback, FeedbackDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null && src.Patient.User != null ? src.Patient.User.Name : null));
        CreateMap<FeedbackCreateDto, Feedback>()
            .ForMember(dest => dest.FeedbackDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<FeedbackUpdateDto, Feedback>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Staff, StaffDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null));
        CreateMap<StaffCreateDto, Staff>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"));
        CreateMap<StaffUpdateDto, Staff>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
