using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicManagement.Web.Pages;
using ClinicManagement.Application.Services;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Domain.Enums;
using ClinicManagement.Domain.Entities;
using FluentAssertions;

namespace ClinicManagement.Web.Tests.Pages;

public class SignUpModelTests
{
    private readonly Mock<IPatientService> _mockPatientService;
    private readonly Mock<ILogger<SignUpModel>> _mockLogger;
    private readonly SignUpModel _signUpModel;

    public SignUpModelTests()
    {
        _mockPatientService = new Mock<IPatientService>();
        _mockLogger = new Mock<ILogger<SignUpModel>>();
        _signUpModel = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithNullPatientService_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SignUpModel(null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SignUpModel(_mockPatientService.Object, null!));
    }

    [Fact]
    public void OnGet_ShouldNotThrowException()
    {
        // Arrange
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Act
        var exception = Record.Exception(() => model.OnGet());

        // Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void Name_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedName = "John Doe";

        // Act
        _signUpModel.Name = expectedName;

        // Assert
        _signUpModel.Name.Should().Be(expectedName);
    }

    [Fact]
    public void Email_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedEmail = "john@example.com";

        // Act
        _signUpModel.Email = expectedEmail;

        // Assert
        _signUpModel.Email.Should().Be(expectedEmail);
    }

    [Fact]
    public void Password_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedPassword = "password123";

        // Act
        _signUpModel.Password = expectedPassword;

        // Assert
        _signUpModel.Password.Should().Be(expectedPassword);
    }

    [Fact]
    public void Phone_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedPhone = "1234567890";

        // Act
        _signUpModel.Phone = expectedPhone;

        // Assert
        _signUpModel.Phone.Should().Be(expectedPhone);
    }

    [Fact]
    public void Address_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedAddress = "123 Main St";

        // Act
        _signUpModel.Address = expectedAddress;

        // Assert
        _signUpModel.Address.Should().Be(expectedAddress);
    }

    [Fact]
    public void BirthDate_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedDate = new DateTime(1990, 1, 1);

        // Act
        _signUpModel.BirthDate = expectedDate;

        // Assert
        _signUpModel.BirthDate.Should().Be(expectedDate);
    }

    [Fact]
    public void Gender_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedGender = Gender.Male;

        // Act
        _signUpModel.Gender = expectedGender;

        // Assert
        _signUpModel.Gender.Should().Be(expectedGender);
    }

    [Fact]
    public void ErrorMessage_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedMessage = "Error occurred";

        // Act
        _signUpModel.ErrorMessage = expectedMessage;

        // Assert
        _signUpModel.ErrorMessage.Should().Be(expectedMessage);
    }

    [Fact]
    public void SuccessMessage_Property_ShouldBeSettable()
    {
        // Arrange
        var expectedMessage = "Success";

        // Act
        _signUpModel.SuccessMessage = expectedMessage;

        // Assert
        _signUpModel.SuccessMessage.Should().Be(expectedMessage);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidModelState_ShouldReturnPage()
    {
        // Arrange
        _signUpModel.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.IsAny<PatientCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithValidData_ShouldCreatePatientAndRedirectToLogin()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "1234567890";
        _signUpModel.Address = "123 Main St";
        _signUpModel.BirthDate = new DateTime(1990, 1, 1);
        _signUpModel.Gender = Gender.Male;

        var expectedPatient = new Patient
        {
            PatientID = 1,
            Name = "John Doe",
            Email = "john@test.com"
        };

        _mockPatientService.Setup(x => x.CreateAsync(It.IsAny<PatientCreateDto>()))
            .ReturnsAsync(expectedPatient);

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("/Login");
        _mockPatientService.Verify(x => x.CreateAsync(It.Is<PatientCreateDto>(dto =>
            dto.Name == "John Doe" &&
            dto.Email == "john@test.com" &&
            dto.Password == "password123" &&
            dto.Phone == "1234567890" &&
            dto.Address == "123 Main St" &&
            dto.BirthDate == new DateTime(1990, 1, 1) &&
            dto.Gender == Gender.Male)), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_WithDuplicateEmail_ShouldReturnPageWithErrorMessage()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "existing@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "1234567890";
        _signUpModel.Address = "123 Main St";
        _signUpModel.BirthDate = new DateTime(1990, 1, 1);
        _signUpModel.Gender = Gender.Male;

        _mockPatientService.Setup(x => x.CreateAsync(It.IsAny<PatientCreateDto>()))
            .ThrowsAsync(new InvalidOperationException("Email already exists"));

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _signUpModel.ErrorMessage.Should().Be("Email already exists");
    }

    [Fact]
    public async Task OnPostAsync_WithServiceException_ShouldReturnPageWithGenericErrorMessage()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "1234567890";
        _signUpModel.Address = "123 Main St";
        _signUpModel.BirthDate = new DateTime(1990, 1, 1);
        _signUpModel.Gender = Gender.Male;

        _mockPatientService.Setup(x => x.CreateAsync(It.IsAny<PatientCreateDto>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _signUpModel.ErrorMessage.Should().Be("An error occurred during registration. Please try again.");
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Error during patient registration")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_WithEmptyName_ShouldNotCallPatientService()
    {
        // Arrange
        _signUpModel.Name = string.Empty;
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.IsAny<PatientCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidEmail_ShouldNotCallPatientService()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "invalidemail";
        _signUpModel.Password = "password123";
        _signUpModel.ModelState.AddModelError("Email", "Invalid email format");

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.IsAny<PatientCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithShortPassword_ShouldNotCallPatientService()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "12345";
        _signUpModel.ModelState.AddModelError("Password", "Password must be at least 6 characters");

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.IsAny<PatientCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithInvalidPhone_ShouldNotCallPatientService()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "invalid";
        _signUpModel.ModelState.AddModelError("Phone", "Invalid phone format");

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.IsAny<PatientCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_WithFemaleGender_ShouldCreatePatientSuccessfully()
    {
        // Arrange
        _signUpModel.Name = "Jane Doe";
        _signUpModel.Email = "jane@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "1234567890";
        _signUpModel.Address = "123 Main St";
        _signUpModel.BirthDate = new DateTime(1990, 1, 1);
        _signUpModel.Gender = Gender.Female;

        var expectedPatient = new Patient
        {
            PatientID = 1,
            Name = "Jane Doe",
            Email = "jane@test.com"
        };

        _mockPatientService.Setup(x => x.CreateAsync(It.IsAny<PatientCreateDto>()))
            .ReturnsAsync(expectedPatient);

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        _mockPatientService.Verify(x => x.CreateAsync(It.Is<PatientCreateDto>(dto =>
            dto.Gender == Gender.Female)), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_SuccessfulRegistration_ShouldLogInformation()
    {
        // Arrange
        _signUpModel.Name = "John Doe";
        _signUpModel.Email = "john@test.com";
        _signUpModel.Password = "password123";
        _signUpModel.Phone = "1234567890";
        _signUpModel.Address = "123 Main St";
        _signUpModel.BirthDate = new DateTime(1990, 1, 1);
        _signUpModel.Gender = Gender.Male;

        var expectedPatient = new Patient
        {
            PatientID = 123,
            Name = "John Doe",
            Email = "john@test.com"
        };

        _mockPatientService.Setup(x => x.CreateAsync(It.IsAny<PatientCreateDto>()))
            .ReturnsAsync(expectedPatient);

        // Act
        var result = await _signUpModel.OnPostAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("New patient registered") && v.ToString()!.Contains("123")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void SignUpModel_ShouldInheritFromPageModel()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Name_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Name.Should().Be(string.Empty);
    }

    [Fact]
    public void Email_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Email.Should().Be(string.Empty);
    }

    [Fact]
    public void Password_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Password.Should().Be(string.Empty);
    }

    [Fact]
    public void Phone_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Phone.Should().Be(string.Empty);
    }

    [Fact]
    public void Address_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.Address.Should().Be(string.Empty);
    }

    [Fact]
    public void ErrorMessage_DefaultValue_ShouldBeNull()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void SuccessMessage_DefaultValue_ShouldBeNull()
    {
        // Arrange & Act
        var model = new SignUpModel(_mockPatientService.Object, _mockLogger.Object);

        // Assert
        model.SuccessMessage.Should().BeNull();
    }
}
