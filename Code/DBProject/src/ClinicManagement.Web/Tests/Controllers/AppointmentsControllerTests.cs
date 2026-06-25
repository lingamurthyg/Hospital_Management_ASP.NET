using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClinicManagement.Web.Controllers;
using ClinicManagement.Application.Services;
using ClinicManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Tests.Controllers
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<AppointmentService> _mockAppointmentService;
        private readonly Mock<ILogger<AppointmentsController>> _mockLogger;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockAppointmentService = new Mock<AppointmentService>();
            _mockLogger = new Mock<ILogger<AppointmentsController>>();
            _controller = new AppointmentsController(_mockAppointmentService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Arrange & Act
            var controller = new AppointmentsController(_mockAppointmentService.Object, _mockLogger.Object);

            // Assert
            controller.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int appointmentId = 1;
            var appointmentDto = new AppointmentDto { AppointmentID = appointmentId };
            _mockAppointmentService.Setup(s => s.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync(appointmentDto);

            // Act
            var result = await _controller.GetById(appointmentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(appointmentDto);
        }

        [Fact]
        public async Task GetById_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int appointmentId = 999;
            _mockAppointmentService.Setup(s => s.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync((AppointmentDto)null);

            // Act
            var result = await _controller.GetById(appointmentId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be($"Appointment with ID {appointmentId} not found");
        }

        [Fact]
        public async Task GetById_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int appointmentId = 1;
            _mockAppointmentService.Setup(s => s.GetAppointmentByIdAsync(appointmentId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetById(appointmentId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetByPatientId_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int patientId = 1;
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto { AppointmentID = 1, PatientID = patientId },
                new AppointmentDto { AppointmentID = 2, PatientID = patientId }
            };
            _mockAppointmentService.Setup(s => s.GetAppointmentsByPatientIdAsync(patientId))
                .ReturnsAsync(appointments);

            // Act
            var result = await _controller.GetByPatientId(patientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(appointments);
        }

        [Fact]
        public async Task GetByPatientId_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int patientId = 1;
            _mockAppointmentService.Setup(s => s.GetAppointmentsByPatientIdAsync(patientId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetByPatientId(patientId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetPendingByDoctorId_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int doctorId = 1;
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto { AppointmentID = 1, DoctorID = doctorId, IsApproved = false }
            };
            _mockAppointmentService.Setup(s => s.GetPendingAppointmentsByDoctorIdAsync(doctorId))
                .ReturnsAsync(appointments);

            // Act
            var result = await _controller.GetPendingByDoctorId(doctorId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(appointments);
        }

        [Fact]
        public async Task GetPendingByDoctorId_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int doctorId = 1;
            _mockAppointmentService.Setup(s => s.GetPendingAppointmentsByDoctorIdAsync(doctorId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetPendingByDoctorId(doctorId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Create_WithValidDto_ReturnsCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateAppointmentDto
            {
                PatientID = 1,
                DoctorID = 1,
                FreeSlotID = 1
            };
            var createdAppointment = new AppointmentDto
            {
                AppointmentID = 1,
                PatientID = createDto.PatientID,
                DoctorID = createDto.DoctorID
            };
            _mockAppointmentService.Setup(s => s.CreateAppointmentAsync(createDto))
                .ReturnsAsync(createdAppointment);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues["id"].Should().Be(createdAppointment.AppointmentID);
        }

        [Fact]
        public async Task Create_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateAppointmentDto();
            _controller.ModelState.AddModelError("PatientID", "Required");

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Create_WithException_ReturnsInternalServerError()
        {
            // Arrange
            var createDto = new CreateAppointmentDto
            {
                PatientID = 1,
                DoctorID = 1,
                FreeSlotID = 1
            };
            _mockAppointmentService.Setup(s => s.CreateAppointmentAsync(createDto))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Approve_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int appointmentId = 1;
            _mockAppointmentService.Setup(s => s.ApproveAppointmentAsync(appointmentId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Approve(appointmentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Approve_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int appointmentId = 999;
            _mockAppointmentService.Setup(s => s.ApproveAppointmentAsync(appointmentId))
                .ThrowsAsync(new InvalidOperationException("Appointment not found"));

            // Act
            var result = await _controller.Approve(appointmentId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Approve_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int appointmentId = 1;
            _mockAppointmentService.Setup(s => s.ApproveAppointmentAsync(appointmentId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Approve(appointmentId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdatePrescription_WithValidData_ReturnsOkResult()
        {
            // Arrange
            int appointmentId = 1;
            var prescriptionDto = new PrescriptionDto
            {
                Disease = "Flu",
                Progress = "Improving",
                Prescription = "Rest and fluids"
            };
            _mockAppointmentService.Setup(s => s.UpdatePrescriptionAsync(
                appointmentId,
                prescriptionDto.Disease,
                prescriptionDto.Progress,
                prescriptionDto.Prescription))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePrescription(appointmentId, prescriptionDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdatePrescription_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int appointmentId = 999;
            var prescriptionDto = new PrescriptionDto
            {
                Disease = "Flu",
                Progress = "Improving",
                Prescription = "Rest and fluids"
            };
            _mockAppointmentService.Setup(s => s.UpdatePrescriptionAsync(
                appointmentId,
                prescriptionDto.Disease,
                prescriptionDto.Progress,
                prescriptionDto.Prescription))
                .ThrowsAsync(new InvalidOperationException("Appointment not found"));

            // Act
            var result = await _controller.UpdatePrescription(appointmentId, prescriptionDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdatePrescription_WithException_ReturnsInternalServerError()
        {
            // Arrange
            int appointmentId = 1;
            var prescriptionDto = new PrescriptionDto
            {
                Disease = "Flu",
                Progress = "Improving",
                Prescription = "Rest and fluids"
            };
            _mockAppointmentService.Setup(s => s.UpdatePrescriptionAsync(
                appointmentId,
                prescriptionDto.Disease,
                prescriptionDto.Progress,
                prescriptionDto.Prescription))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.UpdatePrescription(appointmentId, prescriptionDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public void PrescriptionDto_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var dto = new PrescriptionDto();

            // Assert
            dto.Disease.Should().Be(string.Empty);
            dto.Progress.Should().Be(string.Empty);
            dto.Prescription.Should().Be(string.Empty);
        }

        [Fact]
        public void PrescriptionDto_SetProperties_StoresValues()
        {
            // Arrange
            var dto = new PrescriptionDto();

            // Act
            dto.Disease = "Test Disease";
            dto.Progress = "Test Progress";
            dto.Prescription = "Test Prescription";

            // Assert
            dto.Disease.Should().Be("Test Disease");
            dto.Progress.Should().Be("Test Progress");
            dto.Prescription.Should().Be("Test Prescription");
        }
    }
}
