using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Doctor;

public class HomeModel : PageModel
{
    public string DoctorName { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Index");
        }

        DoctorName = HttpContext.Session.GetString("UserName") ?? "Doctor";
        return Page();
    }
}
