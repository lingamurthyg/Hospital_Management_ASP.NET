using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<StaffService> _logger;

    public StaffService(IStaffRepository repository, IMapper mapper, ILogger<StaffService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<StaffDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<StaffDto>>(items);
    }

    public async Task<StaffDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<StaffDto>(item);
    }
}
