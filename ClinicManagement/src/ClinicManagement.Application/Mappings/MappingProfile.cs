using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Patient, PatientCreateDto>().ReverseMap();
        CreateMap<Patient, PatientUpdateDto>().ReverseMap();
        
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Doctor, DoctorCreateDto>().ReverseMap();
        CreateMap<Doctor, DoctorUpdateDto>().ReverseMap();
        
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<TimeSlot, TimeSlotDto>().ReverseMap();
        CreateMap<Bill, BillDto>().ReverseMap();
        CreateMap<Feedback, FeedbackDto>().ReverseMap();
        CreateMap<Staff, StaffDto>().ReverseMap();
    }
}
