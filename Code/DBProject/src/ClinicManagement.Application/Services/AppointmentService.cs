using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Services
{
    /// <summary>
    /// Service for managing appointment operations
    /// </summary>
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository appointmentRepository, ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found", id);
                    return null;
                }

                return MapToDto(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            try
            {
                var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
                return appointments.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments for patient {PatientId}", patientId);
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentDto>> GetPendingAppointmentsByDoctorIdAsync(int doctorId)
        {
            try
            {
                var appointments = await _appointmentRepository.GetPendingByDoctorIdAsync(doctorId);
                return appointments.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending appointments for doctor {DoctorId}", doctorId);
                throw;
            }
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto)
        {
            try
            {
                var appointment = new Appointment
                {
                    PatientID = createDto.PatientID,
                    DoctorID = createDto.DoctorID,
                    FreeSlotID = createDto.FreeSlotID,
                    AppointmentDate = DateTime.Now,
                    IsApproved = false,
                    IsCompleted = false,
                    FeedbackGiven = false
                };

                var createdAppointment = await _appointmentRepository.AddAsync(appointment);
                _logger.LogInformation("Appointment created successfully with ID {AppointmentId}", createdAppointment.AppointmentID);

                return MapToDto(createdAppointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                throw;
            }
        }

        public async Task ApproveAppointmentAsync(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
                if (appointment == null)
                {
                    throw new InvalidOperationException($"Appointment with ID {appointmentId} not found");
                }

                appointment.IsApproved = true;
                await _appointmentRepository.UpdateAsync(appointment);
                _logger.LogInformation("Appointment {AppointmentId} approved", appointmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving appointment {AppointmentId}", appointmentId);
                throw;
            }
        }

        public async Task UpdatePrescriptionAsync(int appointmentId, string disease, string progress, string prescription)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
                if (appointment == null)
                {
                    throw new InvalidOperationException($"Appointment with ID {appointmentId} not found");
                }

                appointment.Disease = disease;
                appointment.Progress = progress;
                appointment.Prescription = prescription;
                appointment.IsCompleted = true;

                await _appointmentRepository.UpdateAsync(appointment);
                _logger.LogInformation("Prescription updated for appointment {AppointmentId}", appointmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating prescription for appointment {AppointmentId}", appointmentId);
                throw;
            }
        }

        private AppointmentDto MapToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                PatientName = appointment.Patient?.Name ?? string.Empty,
                DoctorID = appointment.DoctorID,
                DoctorName = appointment.Doctor?.Name ?? string.Empty,
                AppointmentDate = appointment.AppointmentDate,
                Timings = appointment.Timings ?? string.Empty,
                IsApproved = appointment.IsApproved,
                IsCompleted = appointment.IsCompleted,
                Disease = appointment.Disease,
                Progress = appointment.Progress,
                Prescription = appointment.Prescription
            };
        }
    }
}
