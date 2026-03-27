using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<TimeSlotService> _logger;

    public TimeSlotService(ITimeSlotRepository repository, IMapper mapper, ILogger<TimeSlotService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TimeSlotDto>>(items);
    }

    public async Task<TimeSlotDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<TimeSlotDto>(item);
    }
}
