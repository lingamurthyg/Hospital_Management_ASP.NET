using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.Pages.Patients;

public class EditModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(IPatientService patientService, ILogger<EditModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [BindProperty]
    public PatientEditModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var patient = await _patientService.GetByIdAsync(id.Value);
            if (patient == null)
            {
                return NotFound();
            }

            Input = new PatientEditModel
            {
                PatientID = patient.PatientID,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                Address = patient.Address,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading patient {PatientId}", id);
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
            var dto = new PatientUpdateDto
            {
                Name = Input.Name,
                Email = Input.Email,
                Phone = Input.Phone,
                Address = Input.Address,
                BirthDate = Input.BirthDate,
                Gender = Input.Gender
            };

            await _patientService.UpdateAsync(Input.PatientID, dto);
            TempData["SuccessMessage"] = "Patient updated successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient {PatientId}", Input.PatientID);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the patient.");
            return Page();
        }
    }

    public class PatientEditModel
    {
        public int PatientID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

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
