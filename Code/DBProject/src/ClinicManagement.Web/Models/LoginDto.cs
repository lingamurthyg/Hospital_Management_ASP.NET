namespace ClinicManagement.Web.Models
{
    /// <summary>
    /// Data Transfer Object for login requests
    /// </summary>
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
