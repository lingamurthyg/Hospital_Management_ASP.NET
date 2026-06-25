using Xunit;
using FluentAssertions;
using ClinicManagement.Web.Models;

namespace ClinicManagement.Web.Tests.Models
{
    public class LoginDtoTests
    {
        [Fact]
        public void Constructor_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var loginDto = new LoginDto();

            // Assert
            loginDto.Should().NotBeNull();
            loginDto.Email.Should().Be(string.Empty);
            loginDto.Password.Should().Be(string.Empty);
        }

        [Fact]
        public void Email_SetValue_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string expectedEmail = "test@example.com";

            // Act
            loginDto.Email = expectedEmail;

            // Assert
            loginDto.Email.Should().Be(expectedEmail);
        }

        [Fact]
        public void Email_SetNull_StoresNull()
        {
            // Arrange
            var loginDto = new LoginDto();

            // Act
            loginDto.Email = null;

            // Assert
            loginDto.Email.Should().BeNull();
        }

        [Fact]
        public void Email_SetEmptyString_StoresEmptyString()
        {
            // Arrange
            var loginDto = new LoginDto();

            // Act
            loginDto.Email = "";

            // Assert
            loginDto.Email.Should().Be("");
        }

        [Fact]
        public void Password_SetValue_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string expectedPassword = "password123";

            // Act
            loginDto.Password = expectedPassword;

            // Assert
            loginDto.Password.Should().Be(expectedPassword);
        }

        [Fact]
        public void Password_SetNull_StoresNull()
        {
            // Arrange
            var loginDto = new LoginDto();

            // Act
            loginDto.Password = null;

            // Assert
            loginDto.Password.Should().BeNull();
        }

        [Fact]
        public void Password_SetEmptyString_StoresEmptyString()
        {
            // Arrange
            var loginDto = new LoginDto();

            // Act
            loginDto.Password = "";

            // Assert
            loginDto.Password.Should().Be("");
        }

        [Fact]
        public void LoginDto_SetBothProperties_StoresBothValues()
        {
            // Arrange
            var loginDto = new LoginDto();
            string expectedEmail = "user@example.com";
            string expectedPassword = "securePassword";

            // Act
            loginDto.Email = expectedEmail;
            loginDto.Password = expectedPassword;

            // Assert
            loginDto.Email.Should().Be(expectedEmail);
            loginDto.Password.Should().Be(expectedPassword);
        }

        [Fact]
        public void LoginDto_WithLongEmail_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string longEmail = new string('a', 100) + "@example.com";

            // Act
            loginDto.Email = longEmail;

            // Assert
            loginDto.Email.Should().Be(longEmail);
            loginDto.Email.Length.Should().BeGreaterThan(100);
        }

        [Fact]
        public void LoginDto_WithLongPassword_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string longPassword = new string('x', 200);

            // Act
            loginDto.Password = longPassword;

            // Assert
            loginDto.Password.Should().Be(longPassword);
            loginDto.Password.Length.Should().Be(200);
        }

        [Fact]
        public void LoginDto_WithSpecialCharactersInEmail_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string specialEmail = "user+test@sub-domain.example.com";

            // Act
            loginDto.Email = specialEmail;

            // Assert
            loginDto.Email.Should().Be(specialEmail);
        }

        [Fact]
        public void LoginDto_WithSpecialCharactersInPassword_StoresValue()
        {
            // Arrange
            var loginDto = new LoginDto();
            string specialPassword = "P@ssw0rd!#$%";

            // Act
            loginDto.Password = specialPassword;

            // Assert
            loginDto.Password.Should().Be(specialPassword);
        }

        [Fact]
        public void LoginDto_ObjectInitializer_SetsProperties()
        {
            // Arrange & Act
            var loginDto = new LoginDto
            {
                Email = "init@example.com",
                Password = "initPassword"
            };

            // Assert
            loginDto.Email.Should().Be("init@example.com");
            loginDto.Password.Should().Be("initPassword");
        }

        [Fact]
        public void LoginDto_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var loginDto1 = new LoginDto { Email = "user1@example.com", Password = "pass1" };
            var loginDto2 = new LoginDto { Email = "user2@example.com", Password = "pass2" };

            // Assert
            loginDto1.Email.Should().NotBe(loginDto2.Email);
            loginDto1.Password.Should().NotBe(loginDto2.Password);
        }
    }
}
