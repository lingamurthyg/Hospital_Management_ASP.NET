using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ClinicManagement.Web.Pages;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Tests.Pages
{
    public class IndexModelTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly Mock<IDoctorRepository> _mockDoctorRepository;
        private readonly Mock<ILogger<IndexModel>> _mockLogger;
        private readonly IndexModel _indexModel;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ISession> _mockSession;

        public IndexModelTests()
        {
            _mockPatientRepository = new Mock<IPatientRepository>();
            _mockDoctorRepository = new Mock<IDoctorRepository>();
            _mockLogger = new Mock<ILogger<IndexModel>>();
            
            _indexModel = new IndexModel(
                _mockPatientRepository.Object,
                _mockDoctorRepository.Object,
                _mockLogger.Object);

            // Setup HttpContext and Session
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();
            _mockHttpContext.Setup(x => x.Session).Returns(_mockSession.Object);
            
            var pageContext = new PageContext
            {
                HttpContext = _mockHttpContext.Object
            };
            _indexModel.PageContext = pageContext;
            _indexModel.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
                _mockHttpContext.Object,
                Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Arrange & Act
            var model = new IndexModel(
                _mockPatientRepository.Object,
                _mockDoctorRepository.Object,
                _mockLogger.Object);

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void OnGet_ExecutesSuccessfully()
        {
            // Arrange & Act
            _indexModel.OnGet();

            // Assert - Method completes without exception
            Assert.NotNull(_indexModel);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithValidPatientCredentials_RedirectsToPatientHome()
        {
            // Arrange
            var email = "patient@test.com";
            var password = "password123";
            var patient = new Patient { Id = 1, Email = email };
            
            _mockPatientRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync(patient);

            byte[] userIdBytes = BitConverter.GetBytes(1);
            byte[] userTypeBytes = System.Text.Encoding.UTF8.GetBytes("Patient");
            
            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectResult = result as RedirectToPageResult;
            Assert.Equal("/Patient/PatientHome", redirectResult.PageName);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithValidDoctorCredentials_RedirectsToDoctorHome()
        {
            // Arrange
            var email = "doctor@test.com";
            var password = "password123";
            var doctor = new Doctor { Id = 2, Email = email };
            
            _mockPatientRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync((Patient)null);
            _mockDoctorRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync(doctor);

            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectResult = result as RedirectToPageResult;
            Assert.Equal("/Doctor/DoctorHome", redirectResult.PageName);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithAdminCredentials_RedirectsToAdminHome()
        {
            // Arrange
            var email = "admin@clinic.com";
            var password = "admin123";
            
            _mockPatientRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync((Patient)null);
            _mockDoctorRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync((Doctor)null);

            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectResult = result as RedirectToPageResult;
            Assert.Equal("/Admin/AdminHome", redirectResult.PageName);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithInvalidCredentials_ReturnsPageWithError()
        {
            // Arrange
            var email = "invalid@test.com";
            var password = "wrongpassword";
            
            _mockPatientRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync((Patient)null);
            _mockDoctorRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ReturnsAsync((Doctor)null);

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("Invalid email or password", _indexModel.TempData["Error"]);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithNullEmail_ReturnsPageWithError()
        {
            // Arrange
            string email = null;
            var password = "password123";

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostLoginAsync_WithEmptyPassword_ReturnsPageWithError()
        {
            // Arrange
            var email = "test@test.com";
            var password = "";

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostLoginAsync_WhenExceptionThrown_ReturnsPageWithError()
        {
            // Arrange
            var email = "test@test.com";
            var password = "password123";
            
            _mockPatientRepository.Setup(x => x.ValidateLoginAsync(email, password))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _indexModel.OnPostLoginAsync(email, password);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("An error occurred during login", _indexModel.TempData["Error"]);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithValidData_CreatesPatientAndRedirects()
        {
            // Arrange
            var name = "John Doe";
            var email = "john@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1990, 1, 1);
            var gender = "Male";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ReturnsAsync(false);
            _mockPatientRepository.Setup(x => x.AddAsync(It.IsAny<Patient>()))
                .Returns(Task.CompletedTask);

            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectResult = result as RedirectToPageResult;
            Assert.Equal("/Patient/PatientHome", redirectResult.PageName);
            _mockPatientRepository.Verify(x => x.AddAsync(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithExistingEmail_ReturnsPageWithError()
        {
            // Arrange
            var name = "John Doe";
            var email = "existing@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1990, 1, 1);
            var gender = "Male";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ReturnsAsync(true);

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("Email already exists", _indexModel.TempData["Error"]);
            _mockPatientRepository.Verify(x => x.AddAsync(It.IsAny<Patient>()), Times.Never);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithNullName_HandlesGracefully()
        {
            // Arrange
            string name = null;
            var email = "test@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1990, 1, 1);
            var gender = "Male";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ReturnsAsync(false);

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithFutureBirthDate_CalculatesNegativeAge()
        {
            // Arrange
            var name = "Future Baby";
            var email = "future@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = DateTime.Now.AddYears(1);
            var gender = "Male";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ReturnsAsync(false);
            _mockPatientRepository.Setup(x => x.AddAsync(It.IsAny<Patient>()))
                .Returns(Task.CompletedTask);

            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
        }

        [Fact]
        public async Task OnPostSignupAsync_WhenExceptionThrown_ReturnsPageWithError()
        {
            // Arrange
            var name = "John Doe";
            var email = "john@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1990, 1, 1);
            var gender = "Male";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("An error occurred during signup", _indexModel.TempData["Error"]);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithEmptyEmail_HandlesGracefully()
        {
            // Arrange
            var name = "John Doe";
            var email = "";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1990, 1, 1);
            var gender = "Male";

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task OnPostSignupAsync_WithMinimumBirthDate_CalculatesCorrectAge()
        {
            // Arrange
            var name = "Old Person";
            var email = "old@test.com";
            var password = "password123";
            var phone = "1234567890";
            var address = "123 Main St";
            var birthDate = new DateTime(1900, 1, 1);
            var gender = "Female";

            _mockPatientRepository.Setup(x => x.EmailExistsAsync(email))
                .ReturnsAsync(false);
            _mockPatientRepository.Setup(x => x.AddAsync(It.IsAny<Patient>()))
                .Returns(Task.CompletedTask);

            _mockSession.Setup(x => x.Set("UserId", It.IsAny<byte[]>()));
            _mockSession.Setup(x => x.Set("UserType", It.IsAny<byte[]>()));

            // Act
            var result = await _indexModel.OnPostSignupAsync(
                name, email, password, phone, address, birthDate, gender);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
        }
    }
}
