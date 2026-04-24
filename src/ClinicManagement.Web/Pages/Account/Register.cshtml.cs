using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterModel> _logger;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string PhoneNumber { get; set; } = string.Empty;

    [BindProperty]
    public DateTime DateOfBirth { get; set; }

    [BindProperty]
    public Gender Gender { get; set; }

    [BindProperty]
    public string Address { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }

    public RegisterModel(IPatientRepository patientRepository, IPasswordHasher passwordHasher, ILogger<RegisterModel> logger)
    {
        _patientRepository = patientRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

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
            var existingPatient = await _patientRepository.GetByEmailAsync(Email);
            if (existingPatient != null)
            {
                ErrorMessage = "Email already exists. Please use a different email.";
                return Page();
            }

            var patient = new Patient
            {
                Name = Name,
                Email = Email,
                Password = _passwordHasher.HashPassword(Password),
                PhoneNumber = PhoneNumber,
                DateOfBirth = DateOfBirth,
                Gender = Gender,
                Address = Address,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = "System"
            };

            await _patientRepository.AddAsync(patient);

            SuccessMessage = "Registration successful! You can now login.";
            _logger.LogInformation("New patient registered: {Email}", Email);

            return RedirectToPage("/Account/Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during patient registration for email: {Email}", Email);
            ErrorMessage = "An error occurred during registration. Please try again.";
            return Page();
        }
    }
}
