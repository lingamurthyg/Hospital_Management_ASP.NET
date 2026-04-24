using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Patients;

public class DetailsModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ClinicDbContext context, ILogger<DetailsModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Patient? Patient { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PatientID == id && m.IsActive);

            if (Patient == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient details for ID: {PatientId}", id);
            return NotFound();
        }
    }
}
