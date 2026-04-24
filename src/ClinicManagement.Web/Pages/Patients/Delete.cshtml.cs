using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Patients;

public class DeleteModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ClinicDbContext context, ILogger<DeleteModel> logger)
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
            _logger.LogError(ex, "Error retrieving patient for delete: {PatientId}", id);
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.IsActive = false;
                patient.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Patient deleted (soft): {PatientId}", id);
            }

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient: {PatientId}", id);
            return Page();
        }
    }
}
