using AutoMapper;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Application.Services;

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
        var patients = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<PatientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var patient = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<PatientDto>(patient);
    }
}
