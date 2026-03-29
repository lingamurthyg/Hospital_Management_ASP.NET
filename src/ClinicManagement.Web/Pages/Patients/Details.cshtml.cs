using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patients;

public class DetailsModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(IPatientService patientService, ILogger<DetailsModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    public PatientDto? Patient { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Patient = await _patientService.GetByIdAsync(id.Value);
            if (Patient == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading patient {PatientId}", id);
            return NotFound();
        }
    }
}
