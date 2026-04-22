using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ClinicManagement.Web.Pages;
using ClinicManagement.Application.Services;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;
using FluentAssertions;

namespace ClinicManagement.Web.Tests.Pages;

public class LoginModelTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<ILogger<LoginModel>> _mockLogger;
    private readonly LoginModel _loginModel;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly Mock<ISession> _mockSession;

    public LoginModelTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockLogger = new Mock<ILogger<LoginModel>>();
        _mockHttpContext = new Mock<HttpContext>();
        _mockSession = new Mock<ISession>();
        
        _mockHttpContext.Setup(x => x.Session).Returns(_mockSession.Object);
        
        _loginModel = new LoginModel(_mockAuthService.Object, _mockLogger.Object)
        {
            PageContext = new PageContext
            {
                HttpContext = _mockHttpContext.Object
            }
        };
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithNullAuthService_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new LoginModel(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new LoginModel(_mockAuthService.Object, null!));
    }

    [Fact]
    public void OnGet_ShouldNotThrowException()
    {
        // Arrange
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Act
        var exception = Record.Exception(() => model.OnGet());

        // Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void Email_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedEmail = "test@example.com";

        // Act
        _loginModel.Email = expectedEmail;

        // Assert
        _loginModel.Email.Should().Be(expectedEmail);
    }

    [Fact]
    public void Password_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedPassword = "password123";

        // Act
        _loginModel.Password = expectedPassword;

        // Assert
        _loginModel.Password.Should().Be(expectedPassword);
    }

    [Fact]
    public void ErrorMessage_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedMessage = "Login failed";

        // Act
        _loginModel.ErrorMessage = expectedMessage;

        // Assert
        _loginModel.ErrorMessage.Should().Be(expectedMessage);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidModelState_ShouldReturnPage()
    {
        // Arrange
        _loginModel.ModelState.AddModelError("Email", "Email is required");

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockAuthService.Verify(x => x.LoginAsync(It.IsAny<LoginDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithValidCredentials_PatientUser_ShouldRedirectToPatientHome()
    {
        // Arrange
        _loginModel.Email = "patient@test.com";
        _loginModel.Password = "password123";

        var loginResult = new LoginResultDto
        {
            Success = true,
            UserId = 1,
            UserType = UserType.Patient,
            Message = "Login successful"
        };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync(loginResult);

        var sessionData = new Dictionary<string, byte[]>();
        _mockSession.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Callback<string, byte[]>((key, value) => sessionData[key] = value);

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("/Patient/Home");
        _mockAuthService.Verify(x => x.LoginAsync(It.Is<LoginDto>(dto => 
            dto.Email == "patient@test.com" && dto.Password == "password123")), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_WithValidCredentials_DoctorUser_ShouldRedirectToDoctorHome()
    {
        // Arrange
        _loginModel.Email = "doctor@test.com";
        _loginModel.Password = "password123";

        var loginResult = new LoginResultDto
        {
            Success = true,
            UserId = 2,
            UserType = UserType.Doctor,
            Message = "Login successful"
        };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync(loginResult);

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("/Doctor/Home");
    }

    [Fact]
    public async Task OnPostAsync_WithValidCredentials_AdminUser_ShouldRedirectToAdminHome()
    {
        // Arrange
        _loginModel.Email = "admin@test.com";
        _loginModel.Password = "password123";

        var loginResult = new LoginResultDto
        {
            Success = true,
            UserId = 3,
            UserType = UserType.Admin,
            Message = "Login successful"
        };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync(loginResult);

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("/Admin/Home");
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidCredentials_ShouldReturnPageWithErrorMessage()
    {
        // Arrange
        _loginModel.Email = "invalid@test.com";
        _loginModel.Password = "wrongpassword";

        var loginResult = new LoginResultDto
        {
            Success = false,
            Message = "Invalid email or password"
        };

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>()))
            .ReturnsAsync(loginResult);

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _loginModel.ErrorMessage.Should().Be("Invalid email or password");
    }

    [Fact]
    public async Task OnPostAsync_WithEmptyEmail_ShouldNotCallAuthService()
    {
        // Arrange
        _loginModel.Email = string.Empty;
        _loginModel.Password = "password123";
        _loginModel.ModelState.AddModelError("Email", "Email is required");

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockAuthService.Verify(x => x.LoginAsync(It.IsAny<LoginDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithEmptyPassword_ShouldNotCallAuthService()
    {
        // Arrange
        _loginModel.Email = "test@test.com";
        _loginModel.Password = string.Empty;
        _loginModel.ModelState.AddModelError("Password", "Password is required");

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockAuthService.Verify(x => x.LoginAsync(It.IsAny<LoginDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidEmailFormat_ShouldNotCallAuthService()
    {
        // Arrange
        _loginModel.Email = "invalidemail";
        _loginModel.Password = "password123";
        _loginModel.ModelState.AddModelError("Email", "Invalid email format");

        // Act
        var result = await _loginModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockAuthService.Verify(x => x.LoginAsync(It.IsAny<LoginDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_AuthServiceThrowsException_ShouldPropagateException()
    {
        // Arrange
        _loginModel.Email = "test@test.com";
        _loginModel.Password = "password123";

        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _loginModel.OnPostAsync());
    }

    [Fact]
    public void LoginModel_ShouldInheritFromPageModel()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Email_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        model.Email.Should().Be(string.Empty);
    }

    [Fact]
    public void Password_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        model.Password.Should().Be(string.Empty);
    }

    [Fact]
    public void ErrorMessage_DefaultValue_ShouldBeNull()
    {
        // Arrange & Act
        var model = new LoginModel(_mockAuthService.Object, _mockLogger.Object);

        // Assert
        model.ErrorMessage.Should().BeNull();
    }
}
