using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.Pages.Patients;

public class CreateModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IPatientService patientService, ILogger<CreateModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [BindProperty]
    public PatientInputModel Input { get; set; } = new();

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
            var dto = new PatientCreateDto
            {
                Name = Input.Name,
                Email = Input.Email,
                Password = Input.Password,
                Phone = Input.Phone,
                Address = Input.Address,
                BirthDate = Input.BirthDate,
                Gender = Input.Gender
            };

            await _patientService.CreateAsync(dto);
            TempData["SuccessMessage"] = "Patient created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating patient");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the patient.");
            return Page();
        }
    }

    public class PatientInputModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;
    }
}
