using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class OtherStaffService : IOtherStaffService
{
    private readonly IOtherStaffRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<OtherStaffService> _logger;

    public OtherStaffService(IOtherStaffRepository repository, IMapper mapper, ILogger<OtherStaffService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<OtherStaffDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all staff");
            var staff = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OtherStaffDto>>(staff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all staff");
            throw;
        }
    }

    public async Task<OtherStaffDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving staff with ID: {StaffId}", id);
            var staff = await _repository.GetByIdAsync(id, cancellationToken);
            if (staff == null)
            {
                _logger.LogWarning("Staff with ID {StaffId} not found", id);
                return null;
            }
            return _mapper.Map<OtherStaffDto>(staff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving staff with ID: {StaffId}", id);
            throw;
        }
    }

    public async Task<OtherStaffDto> CreateAsync(OtherStaffCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new staff: {StaffEmail}", dto.Email);
            var staff = _mapper.Map<OtherStaff>(dto);
            var createdStaff = await _repository.AddAsync(staff, cancellationToken);
            _logger.LogInformation("Staff created successfully with ID: {StaffId}", createdStaff.StaffID);
            return _mapper.Map<OtherStaffDto>(createdStaff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating staff: {StaffEmail}", dto.Email);
            throw;
        }
    }

    public async Task UpdateAsync(int id, OtherStaffUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating staff with ID: {StaffId}", id);
            var existingStaff = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingStaff == null)
            {
                throw new InvalidOperationException($"Staff with ID {id} not found");
            }

            _mapper.Map(dto, existingStaff);
            await _repository.UpdateAsync(existingStaff, cancellationToken);
            _logger.LogInformation("Staff updated successfully: {StaffId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating staff with ID: {StaffId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting staff with ID: {StaffId}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Staff with ID {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Staff deleted successfully: {StaffId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting staff with ID: {StaffId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<OtherStaffDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching staff with term: {SearchTerm}", searchTerm);
            var staff = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<OtherStaffDto>>(staff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching staff with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
