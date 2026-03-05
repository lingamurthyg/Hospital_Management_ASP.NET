using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patient;

public class HomeModel : PageModel
{
    public string PatientName { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Index");
        }

        PatientName = HttpContext.Session.GetString("UserName") ?? "Patient";
        return Page();
    }
}
