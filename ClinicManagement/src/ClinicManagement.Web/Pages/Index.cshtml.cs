using ClinicManagement.Application.DTOs;
using ClinicManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagement.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IAuthService _authService;
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public LoginViewModel LoginInput { get; set; } = new();

    [BindProperty]
    public SignupViewModel SignupInput { get; set; } = new();

    public string? Message { get; set; }

    public IndexModel(IAuthService authService, ILogger<IndexModel> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public void OnGet()
    {
        HttpContext.Session.Clear();
    }

    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (!ModelState.IsValid)
        {
            Message = "Please fill in all required fields";
            return Page();
        }

        var loginDto = new LoginDto
        {
            Email = LoginInput.Email,
            Password = LoginInput.Password
        };

        var result = await _authService.LoginAsync(loginDto);

        if (result.Success)
        {
            HttpContext.Session.SetInt32("UserId", result.UserId);
            HttpContext.Session.SetInt32("UserType", result.UserType);
            HttpContext.Session.SetString("UserName", result.Name);

            return result.UserType switch
            {
                1 => RedirectToPage("/Patient/Home"),
                2 => RedirectToPage("/Doctor/Home"),
                3 => RedirectToPage("/Admin/Home"),
                _ => RedirectToPage("/Index")
            };
        }

        Message = result.Message;
        return Page();
    }

    public async Task<IActionResult> OnPostSignupAsync()
    {
        if (!ModelState.IsValid)
        {
            Message = "Please fill in all required fields";
            return Page();
        }

        var userCreateDto = new UserCreateDto
        {
            Name = SignupInput.Name,
            Email = SignupInput.Email,
            Password = SignupInput.Password,
            PhoneNo = SignupInput.PhoneNo,
            Address = SignupInput.Address,
            Gender = SignupInput.Gender,
            BirthDate = SignupInput.BirthDate,
            UserType = 1
        };

        var result = await _authService.RegisterAsync(userCreateDto);

        if (result.Success)
        {
            HttpContext.Session.SetInt32("UserId", result.UserId);
            HttpContext.Session.SetInt32("UserType", result.UserType);
            HttpContext.Session.SetString("UserName", result.Name);
            return RedirectToPage("/Patient/Home");
        }

        Message = result.Message;
        return Page();
    }

    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class SignupViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
