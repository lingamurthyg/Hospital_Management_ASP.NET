using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using ClinicManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.Pages;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(IAuthService authService, ILogger<LoginModel> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var loginDto = new LoginDto
        {
            Email = Email,
            Password = Password
        };

        var result = await _authService.LoginAsync(loginDto);

        if (result.Success)
        {
            // Store user info in session
            HttpContext.Session.SetInt32("UserId", result.UserId);
            HttpContext.Session.SetInt32("UserType", (int)result.UserType);

            // Redirect based on user type
            return result.UserType switch
            {
                UserType.Patient => RedirectToPage("/Patient/Home"),
                UserType.Doctor => RedirectToPage("/Doctor/Home"),
                UserType.Admin => RedirectToPage("/Admin/Home"),
                _ => RedirectToPage("/Index")
            };
        }

        ErrorMessage = result.Message;
        return Page();
    }
}
