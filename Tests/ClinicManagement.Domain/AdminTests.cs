using Xunit;
using ClinicManagement.Domain.Entities;
using System;

namespace ClinicManagement.Domain.Tests
{
    public class AdminTests
    {
        [Fact]
        public void Admin_Id_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedId = 1;

            // Act
            admin.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, admin.Id);
        }

        [Fact]
        public void Admin_Name_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedName = "Administrator";

            // Act
            admin.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, admin.Name);
        }

        [Fact]
        public void Admin_Email_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedEmail = "admin@clinic.com";

            // Act
            admin.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, admin.Email);
        }

        [Fact]
        public void Admin_Password_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedPassword = "AdminPass123";

            // Act
            admin.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, admin.Password);
        }

        [Fact]
        public void Admin_CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedDate = DateTime.UtcNow;

            // Act
            admin.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, admin.CreatedDate);
        }

        [Fact]
        public void Admin_ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();
            var expectedDate = DateTime.UtcNow;

            // Act
            admin.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, admin.ModifiedDate);
        }

        [Fact]
        public void Admin_ModifiedDate_CanBeNull()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.ModifiedDate = null;

            // Assert
            Assert.Null(admin.ModifiedDate);
        }

        [Fact]
        public void Admin_IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.IsActive = true;

            // Assert
            Assert.True(admin.IsActive);
        }

        [Fact]
        public void Admin_AllProperties_ShouldBeSettable()
        {
            // Arrange & Act
            var admin = new Admin
            {
                Id = 1,
                Name = "John Admin",
                Email = "john.admin@clinic.com",
                Password = "SecurePass456",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow.AddDays(1),
                IsActive = true
            };

            // Assert
            Assert.Equal(1, admin.Id);
            Assert.Equal("John Admin", admin.Name);
            Assert.Equal("john.admin@clinic.com", admin.Email);
            Assert.Equal("SecurePass456", admin.Password);
            Assert.True(admin.IsActive);
        }

        [Fact]
        public void Admin_DefaultValues_ShouldBeCorrect()
        {
            // Arrange & Act
            var admin = new Admin();

            // Assert
            Assert.Equal(string.Empty, admin.Name);
            Assert.Equal(string.Empty, admin.Email);
            Assert.Equal(string.Empty, admin.Password);
            Assert.False(admin.IsActive);
        }

        [Fact]
        public void Admin_Email_ShouldHandleEmptyString()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.Email = "";

            // Assert
            Assert.Equal(string.Empty, admin.Email);
        }

        [Fact]
        public void Admin_Name_ShouldHandleEmptyString()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.Name = "";

            // Assert
            Assert.Equal(string.Empty, admin.Name);
        }

        [Fact]
        public void Admin_Password_ShouldHandleEmptyString()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.Password = "";

            // Assert
            Assert.Equal(string.Empty, admin.Password);
        }

        [Fact]
        public void Admin_IsActive_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            var admin = new Admin();

            // Assert
            Assert.False(admin.IsActive);
        }
    }
}
