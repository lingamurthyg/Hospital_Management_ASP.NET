using Xunit;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ClinicManagement.Web.Pages.Patient;
using Moq;

namespace ClinicManagement.Web.Tests.Pages.Patient
{
    public class PatientHomeModelTests
    {
        private readonly PatientHomeModel _patientHomeModel;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ISession> _mockSession;

        public PatientHomeModelTests()
        {
            _patientHomeModel = new PatientHomeModel();
            
            // Setup HttpContext and Session
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();
            _mockHttpContext.Setup(x => x.Session).Returns(_mockSession.Object);
            
            var pageContext = new PageContext
            {
                HttpContext = _mockHttpContext.Object
            };
            _patientHomeModel.PageContext = pageContext;
        }

        [Fact]
        public void Constructor_CreatesInstance()
        {
            // Arrange & Act
            var model = new PatientHomeModel();

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public void PatientId_DefaultValue_IsZero()
        {
            // Arrange
            var model = new PatientHomeModel();

            // Assert
            Assert.Equal(0, model.PatientId);
        }

        [Fact]
        public void PatientId_CanBeSet()
        {
            // Arrange
            var model = new PatientHomeModel();
            var expectedId = 123;

            // Act
            model.PatientId = expectedId;

            // Assert
            Assert.Equal(expectedId, model.PatientId);
        }

        [Fact]
        public void OnGet_WithValidUserId_SetsPatientId()
        {
            // Arrange
            var userId = 42;
            byte[] userIdBytes = System.BitConverter.GetBytes(userId);
            
            _mockSession.Setup(x => x.TryGetValue("UserId", out userIdBytes))
                .Returns(true);
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_WithNullUserId_SetsPatientIdToZero()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns((int?)null);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(0, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_WithNoSession_SetsPatientIdToZero()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns((int?)null);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(0, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_CalledMultipleTimes_UpdatesPatientId()
        {
            // Arrange
            var firstUserId = 10;
            var secondUserId = 20;
            
            _mockSession.SetupSequence(x => x.GetInt32("UserId"))
                .Returns(firstUserId)
                .Returns(secondUserId);

            // Act
            _patientHomeModel.OnGet();
            var firstId = _patientHomeModel.PatientId;
            
            _patientHomeModel.OnGet();
            var secondId = _patientHomeModel.PatientId;

            // Assert
            Assert.Equal(firstUserId, firstId);
            Assert.Equal(secondUserId, secondId);
        }

        [Fact]
        public void OnGet_WithNegativeUserId_SetsPatientIdToNegativeValue()
        {
            // Arrange
            var userId = -1;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_WithMaxIntUserId_SetsPatientIdCorrectly()
        {
            // Arrange
            var userId = int.MaxValue;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_WithMinIntUserId_SetsPatientIdCorrectly()
        {
            // Arrange
            var userId = int.MinValue;
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(userId);

            // Act
            _patientHomeModel.OnGet();

            // Assert
            Assert.Equal(userId, _patientHomeModel.PatientId);
        }

        [Fact]
        public void OnGet_ExecutesWithoutException()
        {
            // Arrange
            _mockSession.Setup(x => x.GetInt32("UserId"))
                .Returns(1);

            // Act
            var exception = Record.Exception(() => _patientHomeModel.OnGet());

            // Assert
            Assert.Null(exception);
        }
    }
}
