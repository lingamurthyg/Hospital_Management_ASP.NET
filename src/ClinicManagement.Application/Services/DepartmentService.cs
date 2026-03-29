using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper, ILogger<DepartmentService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all departments");
            var departments = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all departments");
            throw;
        }
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving department with ID: {DepartmentId}", id);
            var department = await _repository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                _logger.LogWarning("Department with ID {DepartmentId} not found", id);
                return null;
            }
            return _mapper.Map<DepartmentDto>(department);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving department with ID: {DepartmentId}", id);
            throw;
        }
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new department: {DepartmentName}", dto.Name);
            var department = _mapper.Map<Department>(dto);
            var createdDepartment = await _repository.AddAsync(department, cancellationToken);
            _logger.LogInformation("Department created successfully with ID: {DepartmentId}", createdDepartment.DepartmentID);
            return _mapper.Map<DepartmentDto>(createdDepartment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating department: {DepartmentName}", dto.Name);
            throw;
        }
    }

    public async Task UpdateAsync(int id, DepartmentUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating department with ID: {DepartmentId}", id);
            var existingDepartment = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingDepartment == null)
            {
                throw new InvalidOperationException($"Department with ID {id} not found");
            }

            _mapper.Map(dto, existingDepartment);
            await _repository.UpdateAsync(existingDepartment, cancellationToken);
            _logger.LogInformation("Department updated successfully: {DepartmentId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating department with ID: {DepartmentId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting department with ID: {DepartmentId}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Department with ID {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Department deleted successfully: {DepartmentId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting department with ID: {DepartmentId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<DepartmentDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching departments with term: {SearchTerm}", searchTerm);
            var departments = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching departments with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
