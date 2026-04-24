using ClinicManagement.Domain.Interfaces.Services;
using ClinicManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(IAuthenticationService authService, ILogger<LoginModel> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [BindProperty]
    public LoginViewModel Input { get; set; } = new();

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
            var (success, userId, userType) = await _authService.ValidateLoginAsync(
                Input.Email,
                Input.Password);

            if (success)
            {
                HttpContext.Session.SetInt32("UserId", userId);
                HttpContext.Session.SetInt32("UserType", userType);

                return userType switch
                {
                    1 => RedirectToPage("/Patient/Home"),
                    2 => RedirectToPage("/Doctor/Home"),
                    3 => RedirectToPage("/Admin/Home"),
                    _ => Page()
                };
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
            return Page();
        }
    }
}
