using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Patients;

public class DashboardModel : PageModel
{
    private readonly IAppointmentRepository _appointmentRepository;

    public int AppointmentCount { get; set; }

    public DashboardModel(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToPage("/Account/Login");
        }

        var patientId = int.Parse(userIdString);
        var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
        AppointmentCount = appointments.Count();

        return Page();
    }
}
