using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Patients;

public class EditModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ClinicDbContext context, ILogger<EditModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public Patient Patient { get; set; } = new Patient();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientID == id && m.IsActive);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient for edit: {PatientId}", id);
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var patientToUpdate = await _context.Patients.FindAsync(Patient.PatientID);
            if (patientToUpdate == null)
            {
                return NotFound();
            }

            patientToUpdate.Name = Patient.Name;
            patientToUpdate.Phone = Patient.Phone;
            patientToUpdate.Address = Patient.Address;
            patientToUpdate.Gender = Patient.Gender;
            patientToUpdate.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient updated: {PatientId}", Patient.PatientID);

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient: {PatientId}", Patient.PatientID);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the patient.");
            return Page();
        }
    }
}
