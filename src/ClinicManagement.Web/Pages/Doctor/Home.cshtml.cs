using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Doctor;

public class HomeModel : PageModel
{
    private readonly IDoctorRepository _doctorRepository;

    public HomeModel(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public string DoctorName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        var doctor = await _doctorRepository.GetByIdAsync(userId.Value);
        if (doctor != null)
        {
            DoctorName = doctor.Name;
        }

        return Page();
    }
}
