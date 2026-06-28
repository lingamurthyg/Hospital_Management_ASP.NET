using Xunit;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ClinicManagement.Web.Pages.Doctor;
using Moq;

namespace ClinicManagement.Web.Tests.Pages.Doctor
{
    public class DoctorHomeModelTests
    {
        private readonly DoctorHomeModel _doctorHomeModel;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ISession> _mockSession;

        public DoctorHomeModelTests()
        {
            _doctorHomeModel = new DoctorHomeModel();
            
            // Setup HttpContext and Session
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();
            _mockHttpContext.Setup(x => x.Session).Returns(_mockSession.Object);
            
            var pageContext = new PageContext
            {
                HttpContext = _mockHttpContext.Object
            };
            _doctorHomeModel.PageContext = pageContext;
        }

        [Fact]
        public void Constructor_CreatesInstance()
        {
            // Arrange & Act
            var model = new DoctorHomeModel();

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void DoctorId_DefaultValue_IsZero()
        {
            // Arrange
            var model = new DoctorHomeModel();

            // Assert
            Assert.Equal(0, model.DoctorId);
        }

        [Fact]
        public void DoctorId_CanBeSet()
        {
            // Arrange
            var model = new DoctorHomeModel();
            var expectedId = 456;

            // Act
            model.DoctorId = expectedId;

            // Assert
            Assert.Equal(expectedId, model.DoctorId);
        }

        [Fact]
        public void OnGet_WithValidUserId_SetsDoctorId()
        {
            // Arrange
            var userId = 99;
            byte[] userIdBytes = System.BitConverter.GetBytes(userId);
            
            _mockSession.Setup(x => x.TryGetValue("UserId", out userIdBytes))
                .Returns(true);
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_WithNullUserId_SetsDoctorIdToZero()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns((int?)null);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(0, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_WithNoSession_SetsDoctorIdToZero()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns((int?)null);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(0, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_CalledMultipleTimes_UpdatesDoctorId()
        {
            // Arrange
            var firstUserId = 15;
            var secondUserId = 25;
            
            _mockSession.SetupSequence(x => x.GetInt32("UserId"))
                .Returns(firstUserId)
                .Returns(secondUserId);

            // Act
            _doctorHomeModel.OnGet();
            var firstId = _doctorHomeModel.DoctorId;
            
            _doctorHomeModel.OnGet();
            var secondId = _doctorHomeModel.DoctorId;

            // Assert
            Assert.Equal(firstUserId, firstId);
            Assert.Equal(secondUserId, secondId);
        }

        [Fact]
        public void OnGet_WithNegativeUserId_SetsDoctorIdToNegativeValue()
        {
            // Arrange
            var userId = -5;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_WithMaxIntUserId_SetsDoctorIdCorrectly()
        {
            // Arrange
            var userId = int.MaxValue;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_WithMinIntUserId_SetsDoctorIdCorrectly()
        {
            // Arrange
            var userId = int.MinValue;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void OnGet_ExecutesWithoutException()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(1);

            // Act
            var exception = Record.Exception(() => _doctorHomeModel.OnGet());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OnGet_WithZeroUserId_SetsDoctorIdToZero()
        {
            // Arrange
            var userId = 0;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _doctorHomeModel.OnGet();

            // Assert
            Assert.Equal(0, _doctorHomeModel.DoctorId);
        }

        [Fact]
        public void DoctorId_SetToPositiveValue_ReturnsCorrectValue()
        {
            // Arrange
            var model = new DoctorHomeModel();
            var expectedId = 1000;

            // Act
            model.DoctorId = expectedId;

            // Assert
            Assert.Equal(expectedId, model.DoctorId);
        }

        [Fact]
        public void DoctorId_SetToNegativeValue_ReturnsCorrectValue()
        {
            // Arrange
            var model = new DoctorHomeModel();
            var expectedId = -100;

            // Act
            model.DoctorId = expectedId;

            // Assert
            Assert.Equal(expectedId, model.DoctorId);
        }
    }
}
