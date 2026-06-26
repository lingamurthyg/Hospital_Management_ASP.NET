using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Doctor;

public class DoctorHomeModel : PageModel
{
    public int DoctorId { get; set; }

    public void OnGet()
    {
        DoctorId = HttpContext.Session.GetInt32("UserId") ?? 0;
    }
}
