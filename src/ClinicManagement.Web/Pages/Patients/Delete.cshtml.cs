using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patients;

public class DeleteModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(IPatientService patientService, ILogger<DeleteModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (Patient == null || Patient.PatientID == 0)
        {
            return NotFound();
        }

        try
        {
            await _patientService.DeleteAsync(Patient.PatientID);
            TempData["SuccessMessage"] = "Patient deleted successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient {PatientId}", Patient.PatientID);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the patient.");
            return Page();
        }
    }
}
