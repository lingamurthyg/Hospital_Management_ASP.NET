using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

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
        var doctors = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<DoctorDto>(doctor);
    }
}
