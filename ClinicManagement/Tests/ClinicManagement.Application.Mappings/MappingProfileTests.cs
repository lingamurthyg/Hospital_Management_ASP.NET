using Xunit;
using AutoMapper;
using ClinicManagement.Application.Mappings;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Application.Mappings;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void MappingProfile_Configuration_IsValid()
    {
        // Arrange & Act & Assert
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        Assert.NotNull(mapper);
    }

    [Fact]
    public void Map_User_To_UserDto()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            PhoneNo = "1234567890",
            Address = "123 Main St",
            Gender = "Male",
            BirthDate = new DateTime(1990, 1, 1),
            UserType = 1
        };

        // Act
        var userDto = _mapper.Map<UserDto>(user);

        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(user.Id, userDto.Id);
        Assert.Equal(user.Name, userDto.Name);
        Assert.Equal(user.Email, userDto.Email);
        Assert.Equal(user.PhoneNo, userDto.PhoneNo);
        Assert.Equal(user.Address, userDto.Address);
        Assert.Equal(user.Gender, userDto.Gender);
        Assert.Equal(user.BirthDate, userDto.BirthDate);
        Assert.Equal(user.UserType, userDto.UserType);
    }

    [Fact]
    public void Map_UserCreateDto_To_User()
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            Password = "password123",
            PhoneNo = "9876543210",
            Gender = "Female",
            BirthDate = new DateTime(1995, 5, 15),
            UserType = 1
        };

        // Act
        var user = _mapper.Map<User>(userCreateDto);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userCreateDto.Name, user.Name);
        Assert.Equal(userCreateDto.Email, user.Email);
        Assert.Equal(userCreateDto.Password, user.Password);
        Assert.True(user.IsActive);
        Assert.Equal("System", user.CreatedBy);
    }

    [Fact]
    public void Map_Patient_To_PatientDto()
    {
        // Arrange
        var patient = new Patient
        {
            Id = 1,
            UserId = 10,
            MedicalHistory = "Diabetes",
            User = new User { Name = "Patient Name", Email = "patient@example.com" }
        };

        // Act
        var patientDto = _mapper.Map<PatientDto>(patient);

        // Assert
        Assert.NotNull(patientDto);
        Assert.Equal(patient.Id, patientDto.Id);
        Assert.Equal(patient.UserId, patientDto.UserId);
        Assert.Equal(patient.MedicalHistory, patientDto.MedicalHistory);
        Assert.Equal("Patient Name", patientDto.UserName);
        Assert.Equal("patient@example.com", patientDto.UserEmail);
    }

    [Fact]
    public void Map_Doctor_To_DoctorDto()
    {
        // Arrange
        var doctor = new Doctor
        {
            Id = 1,
            UserId = 10,
            Specialization = "Cardiology",
            Qualification = "MD",
            ConsultationFee = 150.00m,
            AvailableTimings = "9 AM - 5 PM",
            User = new User { Name = "Dr. Smith", Email = "doctor@example.com" }
        };

        // Act
        var doctorDto = _mapper.Map<DoctorDto>(doctor);

        // Assert
        Assert.NotNull(doctorDto);
        Assert.Equal(doctor.Id, doctorDto.Id);
        Assert.Equal(doctor.UserId, doctorDto.UserId);
        Assert.Equal(doctor.Specialization, doctorDto.Specialization);
        Assert.Equal(doctor.Qualification, doctorDto.Qualification);
        Assert.Equal(doctor.ConsultationFee, doctorDto.ConsultationFee);
        Assert.Equal("Dr. Smith", doctorDto.UserName);
        Assert.Equal("doctor@example.com", doctorDto.UserEmail);
    }

    [Fact]
    public void Map_Appointment_To_AppointmentDto()
    {
        // Arrange
        var appointment = new Appointment
        {
            Id = 1,
            PatientId = 5,
            DoctorId = 10,
            AppointmentDate = DateTime.UtcNow,
            Symptoms = "Fever",
            Status = "Pending",
            Patient = new Patient { User = new User { Name = "Patient Name" } },
            Doctor = new Doctor { User = new User { Name = "Doctor Name" } }
        };

        // Act
        var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

        // Assert
        Assert.NotNull(appointmentDto);
        Assert.Equal(appointment.Id, appointmentDto.Id);
        Assert.Equal(appointment.PatientId, appointmentDto.PatientId);
        Assert.Equal(appointment.DoctorId, appointmentDto.DoctorId);
        Assert.Equal("Patient Name", appointmentDto.PatientName);
        Assert.Equal("Doctor Name", appointmentDto.DoctorName);
    }

    [Fact]
    public void Map_Bill_To_BillDto()
    {
        // Arrange
        var bill = new Bill
        {
            Id = 1,
            PatientId = 5,
            AppointmentId = 10,
            Amount = 250.00m,
            BillDate = DateTime.UtcNow,
            Status = "Paid",
            Description = "Consultation",
            Patient = new Patient { User = new User { Name = "Patient Name" } }
        };

        // Act
        var billDto = _mapper.Map<BillDto>(bill);

        // Assert
        Assert.NotNull(billDto);
        Assert.Equal(bill.Id, billDto.Id);
        Assert.Equal(bill.PatientId, billDto.PatientId);
        Assert.Equal(bill.AppointmentId, billDto.AppointmentId);
        Assert.Equal(bill.Amount, billDto.Amount);
        Assert.Equal(bill.Status, billDto.Status);
        Assert.Equal("Patient Name", billDto.PatientName);
    }

    [Fact]
    public void Map_Feedback_To_FeedbackDto()
    {
        // Arrange
        var feedback = new Feedback
        {
            Id = 1,
            PatientId = 5,
            Message = "Great service",
            Rating = 5,
            FeedbackDate = DateTime.UtcNow,
            Patient = new Patient { User = new User { Name = "Patient Name" } }
        };

        // Act
        var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

        // Assert
        Assert.NotNull(feedbackDto);
        Assert.Equal(feedback.Id, feedbackDto.Id);
        Assert.Equal(feedback.PatientId, feedbackDto.PatientId);
        Assert.Equal(feedback.Message, feedbackDto.Message);
        Assert.Equal(feedback.Rating, feedbackDto.Rating);
        Assert.Equal("Patient Name", feedbackDto.PatientName);
    }

    [Fact]
    public void Map_Staff_To_StaffDto()
    {
        // Arrange
        var staff = new Staff
        {
            Id = 1,
            UserId = 10,
            Position = "Nurse",
            Department = "Emergency",
            User = new User { Name = "Staff Name" }
        };

        // Act
        var staffDto = _mapper.Map<StaffDto>(staff);

        // Assert
        Assert.NotNull(staffDto);
        Assert.Equal(staff.Id, staffDto.Id);
        Assert.Equal(staff.UserId, staffDto.UserId);
        Assert.Equal(staff.Position, staffDto.Position);
        Assert.Equal(staff.Department, staffDto.Department);
        Assert.Equal("Staff Name", staffDto.UserName);
    }

    [Fact]
    public void Map_BillCreateDto_To_Bill_SetsBillDateToUtcNow()
    {
        // Arrange
        var billCreateDto = new BillCreateDto
        {
            PatientId = 5,
            AppointmentId = 10,
            Amount = 300.00m,
            Status = "Unpaid"
        };

        // Act
        var bill = _mapper.Map<Bill>(billCreateDto);

        // Assert
        Assert.NotNull(bill);
        Assert.Equal(billCreateDto.PatientId, bill.PatientId);
        Assert.Equal(billCreateDto.AppointmentId, bill.AppointmentId);
        Assert.Equal(billCreateDto.Amount, bill.Amount);
        Assert.True(bill.IsActive);
        Assert.Equal("System", bill.CreatedBy);
    }

    [Fact]
    public void Map_FeedbackCreateDto_To_Feedback_SetsFeedbackDateToUtcNow()
    {
        // Arrange
        var feedbackCreateDto = new FeedbackCreateDto
        {
            PatientId = 5,
            Message = "Excellent",
            Rating = 5
        };

        // Act
        var feedback = _mapper.Map<Feedback>(feedbackCreateDto);

        // Assert
        Assert.NotNull(feedback);
        Assert.Equal(feedbackCreateDto.PatientId, feedback.PatientId);
        Assert.Equal(feedbackCreateDto.Message, feedback.Message);
        Assert.Equal(feedbackCreateDto.Rating, feedback.Rating);
        Assert.True(feedback.IsActive);
        Assert.Equal("System", feedback.CreatedBy);
    }
}
