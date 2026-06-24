using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Controllers
{
    /// <summary>
    /// API Controller for Doctor operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(DoctorService doctorService, ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        /// <summary>
        /// Get all doctors
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all doctors");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound($"Doctor with ID {id} not found");
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctor with ID {DoctorId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get doctors by department
        /// </summary>
        [HttpGet("department/{deptNo}")]
        public async Task<IActionResult> GetByDepartment(int deptNo)
        {
            try
            {
                var doctors = await _doctorService.GetDoctorsByDepartmentAsync(deptNo);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctors for department {DeptNo}", deptNo);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Register a new doctor
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DoctorRegistrationDto registrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var doctor = await _doctorService.RegisterDoctorAsync(registrationDto);
                return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorID }, doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering doctor");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Validate doctor login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var isValid = await _doctorService.ValidateLoginAsync(loginDto.Email, loginDto.Password);
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
