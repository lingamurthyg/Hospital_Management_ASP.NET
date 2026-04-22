using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.Pages;

public class SignUpModel : PageModel
{
    private readonly IPatientService _patientService;
    private readonly ILogger<SignUpModel> _logger;

    public SignUpModel(IPatientService patientService, ILogger<SignUpModel> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [BindProperty]
    [Required]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Address { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public DateTime BirthDate { get; set; }

    [BindProperty]
    [Required]
    public Gender Gender { get; set; }

    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var createDto = new PatientCreateDto
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Phone = Phone,
                Address = Address,
                BirthDate = BirthDate,
                Gender = Gender
            };

            var patient = await _patientService.CreateAsync(createDto);
            SuccessMessage = "Registration successful! Please login.";
            _logger.LogInformation("New patient registered: {PatientId}", patient.PatientID);
            
            return RedirectToPage("/Login");
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during patient registration");
            ErrorMessage = "An error occurred during registration. Please try again.";
            return Page();
        }
    }
}
