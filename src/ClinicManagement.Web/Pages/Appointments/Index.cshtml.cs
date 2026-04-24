using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Data;

namespace ClinicManagement.Web.Pages.Appointments;

public class IndexModel : PageModel
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ClinicDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IList<Appointment> Appointments { get; set; } = new List<Appointment>();

    public async Task OnGetAsync()
    {
        try
        {
            Appointments = await _context.Appointments
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving appointments");
            Appointments = new List<Appointment>();
        }
    }
}
