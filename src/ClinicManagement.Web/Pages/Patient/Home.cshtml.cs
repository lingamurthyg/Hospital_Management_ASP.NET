using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patient;

public class HomeModel : PageModel
{
    private readonly IPatientRepository _patientRepository;

    public HomeModel(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public string PatientName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        var patient = await _patientRepository.GetByIdAsync(userId.Value);
        if (patient != null)
        {
            PatientName = patient.Name;
        }

        return Page();
    }
}
