using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Controllers
{
    /// <summary>
    /// API Controller for Appointment operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(AppointmentService appointmentService, ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        /// <summary>
        /// Get appointment by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get appointments by patient ID
        /// </summary>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments for patient {PatientId}", patientId);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get pending appointments by doctor ID
        /// </summary>
        [HttpGet("doctor/{doctorId}/pending")]
        public async Task<IActionResult> GetPendingByDoctorId(int doctorId)
        {
            try
            {
                var appointments = await _appointmentService.GetPendingAppointmentsByDoctorIdAsync(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending appointments for doctor {DoctorId}", doctorId);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new appointment
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appointment = await _appointmentService.CreateAppointmentAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentID }, appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Approve an appointment
        /// </summary>
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _appointmentService.ApproveAppointmentAsync(id);
                return Ok(new { message = "Appointment approved successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving appointment {AppointmentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update prescription for an appointment
        /// </summary>
        [HttpPut("{id}/prescription")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionDto prescriptionDto)
        {
            try
            {
                await _appointmentService.UpdatePrescriptionAsync(
                    id, 
                    prescriptionDto.Disease, 
                    prescriptionDto.Progress, 
                    prescriptionDto.Prescription);
                return Ok(new { message = "Prescription updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating prescription for appointment {AppointmentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class PrescriptionDto
    {
        public string Disease { get; set; } = string.Empty;
        public string Progress { get; set; } = string.Empty;
        public string Prescription { get; set; } = string.Empty;
    }
}
