using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

/// <summary>
/// Authentication service for user login
/// </summary>
public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
}

public class AuthService : IAuthService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IAdminRepository adminRepository,
        ILogger<AuthService> logger)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _adminRepository = adminRepository;
        _logger = logger;
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Login attempt for email: {Email}", loginDto.Email);

            // Check patient
            var patient = await _patientRepository.GetByEmailAsync(loginDto.Email, cancellationToken);
            if (patient != null && patient.Password == loginDto.Password && patient.IsActive)
            {
                _logger.LogInformation("Patient login successful: {PatientId}", patient.PatientID);
                return new LoginResultDto
                {
                    Success = true,
                    UserId = patient.PatientID,
                    UserType = UserType.Patient,
                    Message = "Login successful"
                };
            }

            // Check doctor
            var doctor = await _doctorRepository.GetByEmailAsync(loginDto.Email, cancellationToken);
            if (doctor != null && doctor.Password == loginDto.Password && doctor.Status)
            {
                _logger.LogInformation("Doctor login successful: {DoctorId}", doctor.DoctorID);
                return new LoginResultDto
                {
                    Success = true,
                    UserId = doctor.DoctorID,
                    UserType = UserType.Doctor,
                    Message = "Login successful"
                };
            }

            // Check admin
            var admin = await _adminRepository.GetByEmailAsync(loginDto.Email, cancellationToken);
            if (admin != null && admin.Password == loginDto.Password && admin.IsActive)
            {
                _logger.LogInformation("Admin login successful: {AdminId}", admin.AdminID);
                return new LoginResultDto
                {
                    Success = true,
                    UserId = admin.AdminID,
                    UserType = UserType.Admin,
                    Message = "Login successful"
                };
            }

            _logger.LogWarning("Login failed for email: {Email}", loginDto.Email);
            return new LoginResultDto
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", loginDto.Email);
            return new LoginResultDto
            {
                Success = false,
                Message = "An error occurred during login"
            };
        }
    }
}

/// <summary>
/// Patient service
/// </summary>
public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PatientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PatientDto> CreateAsync(PatientCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, PatientUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PatientDto>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PatientService> _logger;

    public PatientService(IPatientRepository repository, IMapper mapper, ILogger<PatientService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var patients = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all patients");
            throw;
        }
    }

    public async Task<PatientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var patient = await _repository.GetByIdAsync(id, cancellationToken);
            return patient != null ? _mapper.Map<PatientDto>(patient) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting patient by id: {Id}", id);
            throw;
        }
    }

    public async Task<PatientDto> CreateAsync(PatientCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if email already exists
            if (await _repository.EmailExistsAsync(dto.Email, cancellationToken))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var patient = _mapper.Map<Patient>(dto);
            var created = await _repository.AddAsync(patient, cancellationToken);
            _logger.LogInformation("Patient created: {PatientId}", created.PatientID);
            return _mapper.Map<PatientDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating patient");
            throw;
        }
    }

    public async Task UpdateAsync(int id, PatientUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var patient = await _repository.GetByIdAsync(id, cancellationToken);
            if (patient == null)
            {
                throw new InvalidOperationException("Patient not found");
            }

            patient.Name = dto.Name;
            patient.Phone = dto.Phone;
            patient.Address = dto.Address;
            patient.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(patient, cancellationToken);
            _logger.LogInformation("Patient updated: {PatientId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Patient deleted: {PatientId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PatientDto>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            var patients = await _repository.SearchByNameAsync(name, cancellationToken);
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching patients by name: {Name}", name);
            throw;
        }
    }
}

/// <summary>
/// Doctor service
/// </summary>
public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DoctorDto> CreateAsync(DoctorCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, DoctorUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorDto>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
}

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository repository, IMapper mapper, ILogger<DoctorService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var doctors = await _repository.GetActiveDoctorsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all doctors");
            throw;
        }
    }

    public async Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctor = await _repository.GetByIdAsync(id, cancellationToken);
            return doctor != null ? _mapper.Map<DoctorDto>(doctor) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting doctor by id: {Id}", id);
            throw;
        }
    }

    public async Task<DoctorDto> CreateAsync(DoctorCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if email already exists
            if (await _repository.EmailExistsAsync(dto.Email, cancellationToken))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var doctor = _mapper.Map<Doctor>(dto);
            var created = await _repository.AddAsync(doctor, cancellationToken);
            _logger.LogInformation("Doctor created: {DoctorId}", created.DoctorID);
            return _mapper.Map<DoctorDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating doctor");
            throw;
        }
    }

    public async Task UpdateAsync(int id, DoctorUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctor = await _repository.GetByIdAsync(id, cancellationToken);
            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor not found");
            }

            doctor.Phone = dto.Phone;
            doctor.Address = dto.Address;
            doctor.ChargesPerVisit = dto.ChargesPerVisit;
            doctor.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(doctor, cancellationToken);
            _logger.LogInformation("Doctor updated: {DoctorId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctor = await _repository.GetByIdAsync(id, cancellationToken);
            if (doctor != null)
            {
                doctor.Status = false;
                doctor.ModifiedDate = DateTime.UtcNow;
                await _repository.UpdateAsync(doctor, cancellationToken);
                _logger.LogInformation("Doctor deactivated: {DoctorId}", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting doctor: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> GetByDepartmentAsync(int deptNo, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctors = await _repository.GetByDepartmentAsync(deptNo, cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting doctors by department: {DeptNo}", deptNo);
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            var doctors = await _repository.SearchByNameAsync(name, cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching doctors by name: {Name}", name);
            throw;
        }
    }
}

/// <summary>
/// Appointment service
/// </summary>
public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<AppointmentDto> CreateAsync(AppointmentCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, AppointmentUpdateDto dto, CancellationToken cancellationToken = default);
    Task ApproveAsync(int id, CancellationToken cancellationToken = default);
    Task CancelAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppointmentDto>> GetPendingByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppointmentDto>> GetTodaysByDoctorAsync(int doctorId, CancellationToken cancellationToken = default);
}

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly ISlotRepository _slotRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(
        IAppointmentRepository repository,
        ISlotRepository slotRepository,
        IMapper mapper,
        ILogger<AppointmentService> logger)
    {
        _repository = repository;
        _slotRepository = slotRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointments = await _repository.GetByPatientIdAsync(patientId, cancellationToken);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointments for patient: {PatientId}", patientId);
            throw;
        }
    }

    public async Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointments = await _repository.GetByDoctorIdAsync(doctorId, cancellationToken);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<AppointmentDto> CreateAsync(AppointmentCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var slot = await _slotRepository.GetByIdAsync(dto.SlotID, cancellationToken);
            if (slot == null || !slot.IsAvailable)
            {
                throw new InvalidOperationException("Slot is not available");
            }

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.AppointmentDate = slot.SlotDate;
            appointment.Timings = slot.Timings;

            var created = await _repository.AddAsync(appointment, cancellationToken);

            // Mark slot as unavailable
            slot.IsAvailable = false;
            await _slotRepository.UpdateAsync(slot, cancellationToken);

            _logger.LogInformation("Appointment created: {AppointmentId}", created.AppointmentID);
            return _mapper.Map<AppointmentDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating appointment");
            throw;
        }
    }

    public async Task UpdateAsync(int id, AppointmentUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await _repository.GetByIdAsync(id, cancellationToken);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Disease = dto.Disease;
            appointment.Progress = dto.Progress;
            appointment.Prescription = dto.Prescription;
            appointment.Status = dto.Status;
            appointment.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(appointment, cancellationToken);
            _logger.LogInformation("Appointment updated: {AppointmentId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment: {Id}", id);
            throw;
        }
    }

    public async Task ApproveAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await _repository.GetByIdAsync(id, cancellationToken);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Status = AppointmentStatus.Approved;
            appointment.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(appointment, cancellationToken);
            _logger.LogInformation("Appointment approved: {AppointmentId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error approving appointment: {Id}", id);
            throw;
        }
    }

    public async Task CancelAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await _repository.GetByIdAsync(id, cancellationToken);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(appointment, cancellationToken);

            // Make slot available again
            var slot = await _slotRepository.GetByIdAsync(appointment.SlotID, cancellationToken);
            if (slot != null)
            {
                slot.IsAvailable = true;
                await _slotRepository.UpdateAsync(slot, cancellationToken);
            }

            _logger.LogInformation("Appointment cancelled: {AppointmentId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling appointment: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<AppointmentDto>> GetPendingByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointments = await _repository.GetPendingAppointmentsByDoctorAsync(doctorId, cancellationToken);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting pending appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<IEnumerable<AppointmentDto>> GetTodaysByDoctorAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointments = await _repository.GetTodaysAppointmentsByDoctorAsync(doctorId, cancellationToken);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting today's appointments for doctor: {DoctorId}", doctorId);
            throw;
        }
    }
}
