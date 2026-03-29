using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patients;

public class IndexModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IPatientService patientService, ILogger<IndexModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    public IEnumerable<PatientDto>? Patients { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                Patients = await _patientService.SearchAsync(SearchTerm);
            }
            else
            {
                Patients = await _patientService.GetAllAsync();
            }
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading patients");
            TempData["ErrorMessage"] = "An error occurred while loading patients.";
            return Page();
        }
    }
}
