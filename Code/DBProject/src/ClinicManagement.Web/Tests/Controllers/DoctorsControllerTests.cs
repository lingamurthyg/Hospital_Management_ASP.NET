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
    public class DoctorsControllerTests
    {
        private readonly Mock<DoctorService> _mockDoctorService;
        private readonly Mock<ILogger<DoctorsController>> _mockLogger;
        private readonly DoctorsController _controller;

        public DoctorsControllerTests()
        {
            _mockDoctorService = new Mock<DoctorService>();
            _mockLogger = new Mock<ILogger<DoctorsController>>();
            _controller = new DoctorsController(_mockDoctorService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Arrange & Act
            var controller = new DoctorsController(_mockDoctorService.Object, _mockLogger.Object);

            // Assert
            controller.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAll_WithDoctors_ReturnsOkResult()
        {
            // Arrange
            var doctors = new List<DoctorDto>
            {
                new DoctorDto { DoctorID = 1, Name = "Dr. Smith" },
                new DoctorDto { DoctorID = 2, Name = "Dr. Jones" }
            };
            _mockDoctorService.Setup(s => s.GetAllDoctorsAsync())
                .ReturnsAsync(doctors);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(doctors);
        }

        [Fact]
        public async Task GetAll_WithEmptyList_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var doctors = new List<DoctorDto>();
            _mockDoctorService.Setup(s => s.GetAllDoctorsAsync())
                .ReturnsAsync(doctors);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            (okResult.Value as List<DoctorDto>).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithException_ReturnsInternalServerError()
        {
            // Arrange
            _mockDoctorService.Setup(s => s.GetAllDoctorsAsync())
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
            int doctorId = 1;
            var doctorDto = new DoctorDto { DoctorID = doctorId, Name = "Dr. Smith" };
            _mockDoctorService.Setup(s => s.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(doctorDto);

            // Act
            var result = await _controller.GetById(doctorId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(doctorDto);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int doctorId = 999;
            _mockDoctorService.Setup(s => s.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync((DoctorDto)null);

            // Act
            var result = await _controller.GetById(doctorId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be($"Doctor with ID {doctorId} not found");
        }

        [Fact]
        public async Task GetById_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int doctorId = 1;
            _mockDoctorService.Setup(s => s.GetDoctorByIdAsync(doctorId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetById(doctorId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetByDepartment_WithValidDeptNo_ReturnsOkResult()
        {
            // Arrange
            int deptNo = 1;
            var doctors = new List<DoctorDto>
            {
                new DoctorDto { DoctorID = 1, Name = "Dr. Smith", DepartmentName = "Cardiology" },
                new DoctorDto { DoctorID = 2, Name = "Dr. Jones", DepartmentName = "Cardiology" }
            };
            _mockDoctorService.Setup(s => s.GetDoctorsByDepartmentAsync(deptNo))
                .ReturnsAsync(doctors);

            // Act
            var result = await _controller.GetByDepartment(deptNo);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(doctors);
        }

        [Fact]
        public async Task GetByDepartment_WithNoDoctors_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            int deptNo = 999;
            var doctors = new List<DoctorDto>();
            _mockDoctorService.Setup(s => s.GetDoctorsByDepartmentAsync(deptNo))
                .ReturnsAsync(doctors);

            // Act
            var result = await _controller.GetByDepartment(deptNo);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            (okResult.Value as List<DoctorDto>).Should().BeEmpty();
        }

        [Fact]
        public async Task GetByDepartment_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int deptNo = 1;
            _mockDoctorService.Setup(s => s.GetDoctorsByDepartmentAsync(deptNo))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetByDepartment(deptNo);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Register_WithValidDto_ReturnsCreatedAtAction()
        {
            // Arrange
            var registrationDto = new DoctorRegistrationDto
            {
                Name = "Dr. Smith",
                Email = "smith@example.com",
                Password = "password123",
                DeptNo = 1
            };
            var createdDoctor = new DoctorDto
            {
                DoctorID = 1,
                Name = registrationDto.Name,
                Email = registrationDto.Email
            };
            _mockDoctorService.Setup(s => s.RegisterDoctorAsync(registrationDto))
                .ReturnsAsync(createdDoctor);

            // Act
            var result = await _controller.Register(registrationDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues["id"].Should().Be(createdDoctor.DoctorID);
        }

        [Fact]
        public async Task Register_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var registrationDto = new DoctorRegistrationDto();
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
            var registrationDto = new DoctorRegistrationDto
            {
                Name = "Dr. Smith",
                Email = "existing@example.com",
                Password = "password123",
                DeptNo = 1
            };
            _mockDoctorService.Setup(s => s.RegisterDoctorAsync(registrationDto))
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
            var registrationDto = new DoctorRegistrationDto
            {
                Name = "Dr. Smith",
                Email = "smith@example.com",
                Password = "password123",
                DeptNo = 1
            };
            _mockDoctorService.Setup(s => s.RegisterDoctorAsync(registrationDto))
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
                Email = "doctor@example.com",
                Password = "password123"
            };
            _mockDoctorService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
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
                Email = "doctor@example.com",
                Password = "wrongpassword"
            };
            _mockDoctorService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
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
            _mockDoctorService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
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
                Email = "doctor@example.com",
                Password = ""
            };
            _mockDoctorService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
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
                Email = "doctor@example.com",
                Password = "password123"
            };
            _mockDoctorService.Setup(s => s.ValidateLoginAsync(loginDto.Email, loginDto.Password))
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
