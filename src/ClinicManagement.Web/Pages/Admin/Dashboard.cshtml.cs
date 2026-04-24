using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Admin;

public class DashboardModel : PageModel
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public int PatientCount { get; set; }
    public int DoctorCount { get; set; }
    public int AppointmentCount { get; set; }

    public DashboardModel(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToPage("/Account/Login");
        }

        var patients = await _patientRepository.GetAllAsync();
        PatientCount = patients.Count();

        var doctors = await _doctorRepository.GetAllAsync();
        DoctorCount = doctors.Count();

        var appointments = await _appointmentRepository.GetAllAsync();
        AppointmentCount = appointments.Count();

        return Page();
    }
}
