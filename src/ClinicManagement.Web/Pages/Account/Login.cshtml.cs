using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<LoginModel> _logger;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public LoginModel(IAuthenticationService authService, ILogger<LoginModel> logger)
    {
        _authService = authService;
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
            var (isSuccess, userType, userId) = await _authService.ValidateLoginAsync(Email, Password);

            if (isSuccess && userType.HasValue && userId.HasValue)
            {
                HttpContext.Session.SetString("UserId", userId.Value.ToString());
                HttpContext.Session.SetString("UserType", userType.Value.ToString());
                HttpContext.Session.SetString("UserEmail", Email);

                return userType.Value switch
                {
                    UserType.Patient => RedirectToPage("/Patient/Dashboard"),
                    UserType.Doctor => RedirectToPage("/Doctor/Dashboard"),
                    UserType.Admin => RedirectToPage("/Admin/Dashboard"),
                    _ => RedirectToPage("/Index")
                };
            }

            ErrorMessage = "Invalid email or password";
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", Email);
            ErrorMessage = "An error occurred during login. Please try again.";
            return Page();
        }
    }
}
