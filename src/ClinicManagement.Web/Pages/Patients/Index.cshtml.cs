using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Web.Pages.Patients;

public class IndexModel : PageModel
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IPatientRepository patientRepository, IMapper mapper, ILogger<IndexModel> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<PatientDto> Patients { get; set; } = new List<PatientDto>();
    
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading patients list");
            
            var patients = string.IsNullOrWhiteSpace(SearchTerm)
                ? await _patientRepository.GetAllAsync()
                : await _patientRepository.SearchAsync(SearchTerm);

            Patients = _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading patients");
            ModelState.AddModelError(string.Empty, "An error occurred while loading patients.");
        }
    }
}
