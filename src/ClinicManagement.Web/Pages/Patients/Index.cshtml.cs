using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Patients;

public class IndexModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ClinicDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IList<Patient> Patients { get; set; } = new List<Patient>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            var patientsQuery = _context.Patients.AsNoTracking().Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(SearchString))
            {
                patientsQuery = patientsQuery.Where(p => p.Name.Contains(SearchString));
            }

            Patients = await patientsQuery.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patients");
            Patients = new List<Patient>();
        }
    }
}
