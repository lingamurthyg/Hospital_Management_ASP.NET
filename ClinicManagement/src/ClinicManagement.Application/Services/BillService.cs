using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BillService> _logger;

    public BillService(IBillRepository repository, IMapper mapper, ILogger<BillService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BillDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BillDto>>(items);
    }

    public async Task<BillDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<BillDto>(item);
    }
}
