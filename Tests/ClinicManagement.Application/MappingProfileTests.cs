using Xunit;
using AutoMapper;
using ClinicManagement.Application.Mappings;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using System;

namespace ClinicManagement.Application.Tests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void MappingProfile_Configuration_ShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Patient_ToPatientDto_ShouldMapCorrectly()
        {
            // Arrange
            var patient = new Patient
            {
                Id = 1,
                Name = "John Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Email = "john.doe@email.com",
                PhoneNumber = "+1234567890",
                Gender = "Male",
                Address = "123 Main St",
                IsActive = true
            };

            // Act
            var dto = _mapper.Map<PatientDto>(patient);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(patient.Id, dto.Id);
            Assert.Equal(patient.Name, dto.Name);
            Assert.Equal(patient.BirthDate, dto.BirthDate);
            Assert.Equal(patient.Email, dto.Email);
            Assert.Equal(patient.PhoneNumber, dto.PhoneNumber);
            Assert.Equal(patient.Gender, dto.Gender);
            Assert.Equal(patient.Address, dto.Address);
            Assert.Equal(patient.IsActive, dto.IsActive);
        }

        [Fact]
        public void PatientCreateDto_ToPatient_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new PatientCreateDto
            {
                Name = "Jane Smith",
                BirthDate = new DateTime(1985, 5, 15),
                Email = "jane.smith@email.com",
                Password = "SecurePass123",
                PhoneNumber = "+9876543210",
                Gender = "Female",
                Address = "456 Elm St"
            };

            // Act
            var patient = _mapper.Map<Patient>(dto);

            // Assert
            Assert.NotNull(patient);
            Assert.Equal(dto.Name, patient.Name);
            Assert.Equal(dto.BirthDate, patient.BirthDate);
            Assert.Equal(dto.Email, patient.Email);
            Assert.Equal(dto.Password, patient.Password);
            Assert.Equal(dto.PhoneNumber, patient.PhoneNumber);
            Assert.Equal(dto.Gender, patient.Gender);
            Assert.Equal(dto.Address, patient.Address);
            Assert.True(patient.IsActive);
        }

        [Fact]
        public void Doctor_ToDoctorDto_ShouldMapCorrectly()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = 1,
                Name = "Dr. Smith",
                Email = "dr.smith@clinic.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890",
                Address = "123 Medical Plaza",
                IsActive = true
            };

            // Act
            var dto = _mapper.Map<DoctorDto>(doctor);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(doctor.Id, dto.Id);
            Assert.Equal(doctor.Name, dto.Name);
            Assert.Equal(doctor.Email, dto.Email);
            Assert.Equal(doctor.Specialization, dto.Specialization);
            Assert.Equal(doctor.PhoneNumber, dto.PhoneNumber);
            Assert.Equal(doctor.Address, dto.Address);
            Assert.Equal(doctor.IsActive, dto.IsActive);
        }

        [Fact]
        public void DoctorCreateDto_ToDoctor_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new DoctorCreateDto
            {
                Name = "Dr. Jane Doe",
                Email = "jane.doe@clinic.com",
                Password = "SecurePass456",
                Specialization = "Neurology",
                PhoneNumber = "+9876543210",
                Address = "456 Hospital Rd"
            };

            // Act
            var doctor = _mapper.Map<Doctor>(dto);

            // Assert
            Assert.NotNull(doctor);
            Assert.Equal(dto.Name, doctor.Name);
            Assert.Equal(dto.Email, doctor.Email);
            Assert.Equal(dto.Password, doctor.Password);
            Assert.Equal(dto.Specialization, doctor.Specialization);
            Assert.Equal(dto.PhoneNumber, doctor.PhoneNumber);
            Assert.Equal(dto.Address, doctor.Address);
            Assert.True(doctor.IsActive);
        }

        [Fact]
        public void Appointment_ToAppointmentDto_ShouldMapCorrectly()
        {
            // Arrange
            var appointment = new Appointment
            {
                Id = 1,
                PatientId = 10,
                DoctorId = 5,
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Scheduled",
                Notes = "Regular checkup",
                Patient = new Patient { Name = "John Doe" },
                Doctor = new Doctor { Name = "Dr. Smith" }
            };

            // Act
            var dto = _mapper.Map<AppointmentDto>(appointment);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(appointment.Id, dto.Id);
            Assert.Equal(appointment.PatientId, dto.PatientId);
            Assert.Equal(appointment.DoctorId, dto.DoctorId);
            Assert.Equal(appointment.Status, dto.Status);
            Assert.Equal(appointment.Notes, dto.Notes);
            Assert.Equal("John Doe", dto.PatientName);
            Assert.Equal("Dr. Smith", dto.DoctorName);
        }

        [Fact]
        public void AppointmentCreateDto_ToAppointment_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new AppointmentCreateDto
            {
                PatientId = 10,
                DoctorId = 5,
                AppointmentDate = DateTime.UtcNow.AddDays(2),
                Notes = "Follow-up appointment"
            };

            // Act
            var appointment = _mapper.Map<Appointment>(dto);

            // Assert
            Assert.NotNull(appointment);
            Assert.Equal(dto.PatientId, appointment.PatientId);
            Assert.Equal(dto.DoctorId, appointment.DoctorId);
            Assert.Equal(dto.Notes, appointment.Notes);
            Assert.Equal("Pending", appointment.Status);
            Assert.True(appointment.IsActive);
        }

        [Fact]
        public void PatientUpdateDto_ToPatient_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new PatientUpdateDto
            {
                Name = "Updated Name",
                PhoneNumber = "+1111111111",
                Address = "789 Oak St"
            };

            // Act
            var patient = _mapper.Map<Patient>(dto);

            // Assert
            Assert.NotNull(patient);
            Assert.Equal(dto.Name, patient.Name);
            Assert.Equal(dto.PhoneNumber, patient.PhoneNumber);
            Assert.Equal(dto.Address, patient.Address);
        }

        [Fact]
        public void DoctorUpdateDto_ToDoctor_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new DoctorUpdateDto
            {
                Name = "Dr. Updated",
                Specialization = "Orthopedics",
                PhoneNumber = "+1111111111",
                Address = "789 Clinic Ave"
            };

            // Act
            var doctor = _mapper.Map<Doctor>(dto);

            // Assert
            Assert.NotNull(doctor);
            Assert.Equal(dto.Name, doctor.Name);
            Assert.Equal(dto.Specialization, doctor.Specialization);
            Assert.Equal(dto.PhoneNumber, doctor.PhoneNumber);
            Assert.Equal(dto.Address, doctor.Address);
        }

        [Fact]
        public void AppointmentUpdateDto_ToAppointment_ShouldMapCorrectly()
        {
            // Arrange
            var dto = new AppointmentUpdateDto
            {
                AppointmentDate = DateTime.UtcNow.AddDays(3),
                Status = "Confirmed",
                Notes = "Patient confirmed"
            };

            // Act
            var appointment = _mapper.Map<Appointment>(dto);

            // Assert
            Assert.NotNull(appointment);
            Assert.Equal(dto.Status, appointment.Status);
            Assert.Equal(dto.Notes, appointment.Notes);
        }
    }
}
