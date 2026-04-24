using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Patients;

public class CreateModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ClinicDbContext context, ILogger<CreateModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public Patient Patient { get; set; } = new Patient();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            Patient.CreatedDate = DateTime.UtcNow;
            Patient.IsActive = true;
            Patient.Password = BCrypt.Net.BCrypt.HashPassword(Patient.Password);

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient created: {PatientId}", Patient.PatientID);

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating patient");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the patient.");
            return Page();
        }
    }
}
