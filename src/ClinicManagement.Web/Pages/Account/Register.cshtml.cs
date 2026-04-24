using ClinicManagement.Domain.Interfaces.Services;
using ClinicManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(IAuthenticationService authService, ILogger<RegisterModel> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [BindProperty]
    public RegisterViewModel Input { get; set; } = new();

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
            var (success, patientId, message) = await _authService.RegisterPatientAsync(
                Input.Name,
                Input.BirthDate,
                Input.Email,
                Input.Password,
                Input.PhoneNumber,
                Input.Gender,
                Input.Address);

            if (success)
            {
                HttpContext.Session.SetInt32("UserId", patientId);
                HttpContext.Session.SetInt32("UserType", 1);
                return RedirectToPage("/Patient/Home");
            }

            ModelState.AddModelError(string.Empty, message);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
            return Page();
        }
    }
}
