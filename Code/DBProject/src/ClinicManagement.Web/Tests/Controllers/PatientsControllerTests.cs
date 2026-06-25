using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClinicManagement.Web.Controllers;
using ClinicManagement.Application.Services;
using ClinicManagement.Application.DTOs;
using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Tests.Controllers
{
    public class PatientsControllerTests
    {
        private readonly Mock<PatientService> _mockPatientService;
        private readonly Mock<ILogger<PatientsController>> _mockLogger;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _mockPatientService = new Mock<PatientService>();
            _mockLogger = new Mock<ILogger<PatientsController>>();
            _controller = new PatientsController(_mockPatientService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Arrange & Act
            var controller = new PatientsController(_mockPatientService.Object, _mockLogger.Object);

            // Assert
            controller.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAll_WithPatients_ReturnsOkResult()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { PatientID = 1, Name = "John Doe" },
                new PatientDto { PatientID = 2, Name = "Jane Smith" }
            };
            _mockPatientService.Setup(s => s.GetAllPatientsAsync())
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(patients);
        }

        [Fact]
        public async Task GetAll_WithEmptyList_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var patients = new List<PatientDto>();
            _mockPatientService.Setup(s => s.GetAllPatientsAsync())
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            (okResult.Value as List<PatientDto>).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithException_ReturnsInternalServerError()
        {
            // Arrange
            _mockPatientService.Setup(s => s.GetAllPatientsAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int patientId = 1;
            var patientDto = new PatientDto { PatientID = patientId, Name = "John Doe" };
            _mockPatientService.Setup(s => s.GetPatientByIdAsync(patientId))
                .ReturnsAsync(patientDto);

            // Act
            var result = await _controller.GetById(patientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(patientDto);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int patientId = 999;
            _mockPatientService.Setup(s => s.GetPatientByIdAsync(patientId))
                .ReturnsAsync((PatientDto)null);

            // Act
            var result = await _controller.GetById(patientId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be($"Patient with ID {patientId} not found");
        }

        [Fact]
        public async Task GetById_WithZeroId_ReturnsNotFound()
        {
            // Arrange
            int patientId = 0;
            _mockPatientService.Setup(s => s.GetPatientByIdAsync(patientId))
                .ReturnsAsync((PatientDto)null);

            // Act
            var result = await _controller.GetById(patientId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetById_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int patientId = 1;
            _mockPatientService.Setup(s => s.GetPatientByIdAsync(patientId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetById(patientId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Search_WithValidName_ReturnsOkResult()
        {
            // Arrange
            string searchName = "John";
            var patients = new List<PatientDto>
            {
                new PatientDto { PatientID = 1, Name = "John Doe" },
                new PatientDto { PatientID = 2, Name = "John Smith" }
            };
            _mockPatientService.Setup(s => s.SearchPatientsByNameAsync(searchName))
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.Search(searchName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(patients);
        }

        [Fact]
        public async Task Search_WithNoMatches_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            string searchName = "NonExistent";
            var patients = new List<PatientDto>();
            _mockPatientService.Setup(s => s.SearchPatientsByNameAsync(searchName))
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.Search(searchName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            (okResult.Value as List<PatientDto>).Should().BeEmpty();
        }

        [Fact]
        public async Task Search_WithEmptyString_ReturnsOkResult()
        {
            // Arrange
            string searchName = "";
            var patients = new List<PatientDto>();
            _mockPatientService.Setup(s => s.SearchPatientsByNameAsync(searchName))
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.Search(searchName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Search_WithException_ReturnsInternalServerError()
        {
            // Arrange
            string searchName = "John";
            _mockPatientService.Setup(s => s.SearchPatientsByNameAsync(searchName))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Search(searchName);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Register_WithValidDto_ReturnsCreatedAtAction()
        {
            // Arrange
            var registrationDto = new PatientRegistrationDto
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123",
                Phone = "1234567890"
            };
            var createdPatient = new PatientDto
            {
                PatientID = 1,
                Name = registrationDto.Name,
                Email = registrationDto.Email
            };
            _mockPatientService.Setup(s => s.RegisterPatientAsync(registrationDto))
                .ReturnsAsync(createdPatient);

            // Act
            var result = await _controller.Register(registrationDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues["id"].Should().Be(createdPatient.PatientID);
        }

        [Fact]
        public async Task Register_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var registrationDto = new PatientRegistrationDto();
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.Register(registrationDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
        {
            // Arrange
            var registrationDto = new PatientRegistrationDto
            {
                Name = "John Doe",
                Email = "existing@example.com",
                Password = "password123",
                Phone = "1234567890"
            };
            _mockPatientService.Setup(s => s.RegisterPatientAsync(registrationDto))
                .ThrowsAsync(new InvalidOperationException("Email already exists"));

            // Act
            var result = await _controller.Register(registrationDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Email already exists");
        }

        [Fact]
        public async Task Register_WithException_ReturnsInternalServerError()
        {
            // Arrange
            var registrationDto = new PatientRegistrationDto
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123",
                Phone = "1234567890"
            };
            _mockPatientService.Setup(s => s.RegisterPatientAsync(registrationDto))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Register(registrationDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "patient@example.com",
                Password = "password123"
            };
            _mockPatientService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "patient@example.com",
                Password = "wrongpassword"
            };
            _mockPatientService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().Be("Invalid email or password");
        }

        [Fact]
        public async Task Login_WithNullEmail_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = null,
                Password = "password123"
            };
            _mockPatientService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Login_WithEmptyPassword_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "patient@example.com",
                Password = ""
            };
            _mockPatientService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Login_WithException_ReturnsInternalServerError()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "patient@example.com",
                Password = "password123"
            };
            _mockPatientService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }
    }
}
