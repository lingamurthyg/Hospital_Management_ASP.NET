using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Doctors;

public class IndexModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ClinicDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IList<Doctor> Doctors { get; set; } = new List<Doctor>();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            IQueryable<Doctor> doctorsQuery = _context.Doctors
                .AsNoTracking()
                .Where(d => d.Status)
                .Include(d => d.Department);

            if (!string.IsNullOrEmpty(SearchString))
            {
                doctorsQuery = doctorsQuery.Where(d => d.Name.Contains(SearchString) ||
                                                      d.Specialization.Contains(SearchString));
            }

            Doctors = await doctorsQuery.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving doctors");
            Doctors = new List<Doctor>();
        }
    }
}
