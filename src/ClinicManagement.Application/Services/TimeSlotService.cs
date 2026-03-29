using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<TimeSlotService> _logger;

    public TimeSlotService(ITimeSlotRepository repository, IMapper mapper, ILogger<TimeSlotService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all time slots");
            var timeSlots = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all time slots");
            throw;
        }
    }

    public async Task<TimeSlotDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving time slot with ID: {TimeSlotId}", id);
            var timeSlot = await _repository.GetByIdAsync(id, cancellationToken);
            if (timeSlot == null)
            {
                _logger.LogWarning("Time slot with ID {TimeSlotId} not found", id);
                return null;
            }
            return _mapper.Map<TimeSlotDto>(timeSlot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving time slot with ID: {TimeSlotId}", id);
            throw;
        }
    }

    public async Task<TimeSlotDto> CreateAsync(TimeSlotCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new time slot for doctor {DoctorId}", dto.DoctorID);
            var timeSlot = _mapper.Map<TimeSlot>(dto);
            var createdTimeSlot = await _repository.AddAsync(timeSlot, cancellationToken);
            _logger.LogInformation("Time slot created successfully with ID: {TimeSlotId}", createdTimeSlot.TimeSlotID);
            return _mapper.Map<TimeSlotDto>(createdTimeSlot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating time slot");
            throw;
        }
    }

    public async Task UpdateAsync(int id, TimeSlotUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating time slot with ID: {TimeSlotId}", id);
            var existingTimeSlot = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingTimeSlot == null)
            {
                throw new InvalidOperationException($"Time slot with ID {id} not found");
            }

            _mapper.Map(dto, existingTimeSlot);
            await _repository.UpdateAsync(existingTimeSlot, cancellationToken);
            _logger.LogInformation("Time slot updated successfully: {TimeSlotId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating time slot with ID: {TimeSlotId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting time slot with ID: {TimeSlotId}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Time slot with ID {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Time slot deleted successfully: {TimeSlotId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting time slot with ID: {TimeSlotId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<TimeSlotDto>> GetByDoctorIdAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving time slots for doctor: {DoctorId}", doctorId);
            var timeSlots = await _repository.GetByDoctorIdAsync(doctorId, cancellationToken);
            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving time slots for doctor: {DoctorId}", doctorId);
            throw;
        }
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAvailableSlotsAsync(int doctorId, DateTime date, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving available time slots for doctor {DoctorId} on {Date}", doctorId, date);
            var timeSlots = await _repository.GetAvailableSlotsAsync(doctorId, date, cancellationToken);
            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving available time slots");
            throw;
        }
    }
}
