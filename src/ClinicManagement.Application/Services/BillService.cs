using AutoMapper;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BillService> _logger;

    public BillService(IBillRepository repository, IMapper mapper, ILogger<BillService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<BillDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all bills");
            var bills = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BillDto>>(bills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all bills");
            throw;
        }
    }

    public async Task<BillDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving bill with ID: {BillId}", id);
            var bill = await _repository.GetByIdAsync(id, cancellationToken);
            if (bill == null)
            {
                _logger.LogWarning("Bill with ID {BillId} not found", id);
                return null;
            }
            return _mapper.Map<BillDto>(bill);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bill with ID: {BillId}", id);
            throw;
        }
    }

    public async Task<BillDto> CreateAsync(BillCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new bill for patient {PatientId}", dto.PatientID);
            var bill = _mapper.Map<Bill>(dto);
            var createdBill = await _repository.AddAsync(bill, cancellationToken);
            _logger.LogInformation("Bill created successfully with ID: {BillId}", createdBill.BillID);
            return _mapper.Map<BillDto>(createdBill);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bill");
            throw;
        }
    }

    public async Task UpdateAsync(int id, BillUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating bill with ID: {BillId}", id);
            var existingBill = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingBill == null)
            {
                throw new InvalidOperationException($"Bill with ID {id} not found");
            }

            _mapper.Map(dto, existingBill);
            await _repository.UpdateAsync(existingBill, cancellationToken);
            _logger.LogInformation("Bill updated successfully: {BillId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating bill with ID: {BillId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting bill with ID: {BillId}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new InvalidOperationException($"Bill with ID {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Bill deleted successfully: {BillId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting bill with ID: {BillId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<BillDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching bills with term: {SearchTerm}", searchTerm);
            var bills = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<BillDto>>(bills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching bills with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<BillDto>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving bills for patient: {PatientId}", patientId);
            var bills = await _repository.GetByPatientIdAsync(patientId, cancellationToken);
            return _mapper.Map<IEnumerable<BillDto>>(bills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bills for patient: {PatientId}", patientId);
            throw;
        }
    }

    public async Task<BillDto?> GetByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving bill for appointment: {AppointmentId}", appointmentId);
            var bill = await _repository.GetByAppointmentIdAsync(appointmentId, cancellationToken);
            if (bill == null)
            {
                _logger.LogWarning("Bill for appointment {AppointmentId} not found", appointmentId);
                return null;
            }
            return _mapper.Map<BillDto>(bill);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bill for appointment: {AppointmentId}", appointmentId);
            throw;
        }
    }
}
