using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Controllers
{
    /// <summary>
    /// API Controller for Patient operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(PatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var patients = await _patientService.GetAllPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all patients");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get patient by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found");
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient with ID {PatientId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Search patients by name
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            try
            {
                var patients = await _patientService.SearchPatientsByNameAsync(name);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching patients by name {Name}", name);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Register a new patient
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PatientRegistrationDto registrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var patient = await _patientService.RegisterPatientAsync(registrationDto);
                return CreatedAtAction(nameof(GetById), new { id = patient.PatientID }, patient);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering patient");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Validate patient login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var isValid = await _patientService.ValidateLoginAsync(loginDto.Email, loginDto.Password);
                if (!isValid)
                {
                    return Unauthorized("Invalid email or password");
                }
                return Ok(new { message = "Login successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
