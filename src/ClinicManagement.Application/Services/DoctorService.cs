using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository repository, IMapper mapper, ILogger<DoctorService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<DoctorDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all doctors");
            var doctors = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all doctors");
            throw;
        }
    }

    public async Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving doctor with ID: {DoctorId}", id);
            var doctor = await _repository.GetByIdAsync(id, cancellationToken);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {DoctorId} not found", id);
                return null;
            }
            return _mapper.Map<DoctorDto>(doctor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctor with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task<DoctorDto> CreateAsync(DoctorCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new doctor: {DoctorEmail}", dto.Email);
            var existingDoctor = await _repository.GetByEmailAsync(dto.Email, cancellationToken);
            if (existingDoctor != null)
            {
                throw new InvalidOperationException($"Doctor with email {dto.Email} already exists");
            }

            var doctor = _mapper.Map<Doctor>(dto);
            var createdDoctor = await _repository.AddAsync(doctor, cancellationToken);
            _logger.LogInformation("Doctor created successfully with ID: {DoctorId}", createdDoctor.DoctorID);
            return _mapper.Map<DoctorDto>(createdDoctor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating doctor: {DoctorEmail}", dto.Email);
            throw;
        }
    }

    public async Task UpdateAsync(int id, DoctorUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating doctor with ID: {DoctorId}", id);
            var existingDoctor = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingDoctor == null)
            {
                throw new InvalidOperationException($"Doctor with ID {id} not found");
            }

            _mapper.Map(dto, existingDoctor);
            await _repository.UpdateAsync(existingDoctor, cancellationToken);
            _logger.LogInformation("Doctor updated successfully: {DoctorId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting doctor with ID: {DoctorId}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Doctor with ID {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Doctor deleted successfully: {DoctorId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting doctor with ID: {DoctorId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching doctors with term: {SearchTerm}", searchTerm);
            var doctors = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching doctors with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> GetByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving doctors for department: {DepartmentId}", departmentId);
            var doctors = await _repository.GetByDepartmentAsync(departmentId, cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctors for department: {DepartmentId}", departmentId);
            throw;
        }
    }
}
