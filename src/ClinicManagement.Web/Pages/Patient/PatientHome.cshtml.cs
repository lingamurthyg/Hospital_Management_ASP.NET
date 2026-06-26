using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patient;

public class PatientHomeModel : PageModel
{
    public int PatientId { get; set; }

    public void OnGet()
    {
        PatientId = HttpContext.Session.GetInt32("UserId") ?? 0;
    }
}
