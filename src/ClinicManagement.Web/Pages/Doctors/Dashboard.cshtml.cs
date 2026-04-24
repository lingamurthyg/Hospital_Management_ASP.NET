using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Doctors;

public class DashboardModel : PageModel
{
    private readonly IAppointmentRepository _appointmentRepository;

    public int PendingAppointmentCount { get; set; }
    public int TotalAppointmentCount { get; set; }

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

        var doctorId = int.Parse(userIdString);
        var allAppointments = await _appointmentRepository.GetByDoctorIdAsync(doctorId);
        TotalAppointmentCount = allAppointments.Count();

        var pendingAppointments = await _appointmentRepository.GetPendingAppointmentsByDoctorAsync(doctorId);
        PendingAppointmentCount = pendingAppointments.Count();

        return Page();
    }
}
