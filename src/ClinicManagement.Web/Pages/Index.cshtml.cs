using ClinicManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        ILogger<IndexModel> logger)
    {
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostLoginAsync(string email, string password)
    {
        try
        {
            // Check patient login
            var patient = await _patientRepository.ValidateLoginAsync(email, password);
            if (patient != null)
            {
                HttpContext.Session.SetInt32("UserId", patient.Id);
                HttpContext.Session.SetString("UserType", "Patient");
                return RedirectToPage("/Patient/PatientHome");
            }

            // Check doctor login
            var doctor = await _doctorRepository.ValidateLoginAsync(email, password);
            if (doctor != null)
            {
                HttpContext.Session.SetInt32("UserId", doctor.Id);
                HttpContext.Session.SetString("UserType", "Doctor");
                return RedirectToPage("/Doctor/DoctorHome");
            }

            // Check admin login (hardcoded for now)
            if (email == "admin@clinic.com" && password == "admin123")
            {
                HttpContext.Session.SetInt32("UserId", 1);
                HttpContext.Session.SetString("UserType", "Admin");
                return RedirectToPage("/Admin/AdminHome");
            }

            TempData["Error"] = "Invalid email or password";
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            TempData["Error"] = "An error occurred during login";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostSignupAsync(
        string name, string email, string password, string phone, 
        string address, DateTime birthDate, string gender)
    {
        try
        {
            // Check if email already exists
            if (await _patientRepository.EmailExistsAsync(email))
            {
                TempData["Error"] = "Email already exists";
                return Page();
            }

            var patient = new Domain.Entities.Patient
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone,
                Address = address,
                BirthDate = birthDate,
                Gender = gender,
                Age = DateTime.Now.Year - birthDate.Year,
                CreatedBy = "System"
            };

            await _patientRepository.AddAsync(patient);

            HttpContext.Session.SetInt32("UserId", patient.Id);
            HttpContext.Session.SetString("UserType", "Patient");

            return RedirectToPage("/Patient/PatientHome");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during signup");
            TempData["Error"] = "An error occurred during signup";
            return Page();
        }
    }
}
