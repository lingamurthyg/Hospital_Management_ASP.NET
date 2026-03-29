using Xunit;
using System;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests
{
    public class OtherStaffTests
    {
        [Fact]
        public void Constructor_ShouldInitializeOtherStaffWithDefaultValues()
        {
            // Arrange & Act
            var staff = new OtherStaff();

            // Assert
            Assert.Equal(0, staff.StaffID);
            Assert.Equal(string.Empty, staff.Name);
            Assert.Null(staff.Phone);
            Assert.Null(staff.Address);
            Assert.Equal(string.Empty, staff.Role);
            Assert.Equal(string.Empty, staff.Email);
            Assert.Equal(string.Empty, staff.Password);
            Assert.Equal(default(DateTime), staff.CreatedDate);
            Assert.Null(staff.ModifiedDate);
            Assert.False(staff.IsActive);
        }

        [Fact]
        public void StaffID_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            int expectedId = 50;

            // Act
            staff.StaffID = expectedId;

            // Assert
            Assert.Equal(expectedId, staff.StaffID);
        }

        [Fact]
        public void Name_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedName = "Alice Johnson";

            // Act
            staff.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, staff.Name);
        }

        [Fact]
        public void Name_ShouldAcceptEmptyString()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, staff.Name);
        }

        [Fact]
        public void Phone_ShouldAcceptNullValue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Phone = null;

            // Assert
            Assert.Null(staff.Phone);
        }

        [Fact]
        public void Phone_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedPhone = "555-9999";

            // Act
            staff.Phone = expectedPhone;

            // Assert
            Assert.Equal(expectedPhone, staff.Phone);
        }

        [Fact]
        public void Address_ShouldAcceptNullValue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Address = null;

            // Assert
            Assert.Null(staff.Address);
        }

        [Fact]
        public void Address_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedAddress = "789 Elm Street";

            // Act
            staff.Address = expectedAddress;

            // Assert
            Assert.Equal(expectedAddress, staff.Address);
        }

        [Fact]
        public void Role_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedRole = "Nurse";

            // Act
            staff.Role = expectedRole;

            // Assert
            Assert.Equal(expectedRole, staff.Role);
        }

        [Fact]
        public void Role_ShouldAcceptEmptyString()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Role = string.Empty;

            // Assert
            Assert.Equal(string.Empty, staff.Role);
        }

        [Fact]
        public void Email_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedEmail = "alice.johnson@clinic.com";

            // Act
            staff.Email = expectedEmail;

            // Assert
            Assert.Equal(expectedEmail, staff.Email);
        }

        [Fact]
        public void Email_ShouldAcceptEmptyString()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Email = string.Empty;

            // Assert
            Assert.Equal(string.Empty, staff.Email);
        }

        [Fact]
        public void Password_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            string expectedPassword = "SecurePass123";

            // Act
            staff.Password = expectedPassword;

            // Assert
            Assert.Equal(expectedPassword, staff.Password);
        }

        [Fact]
        public void Password_ShouldAcceptEmptyString()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.Password = string.Empty;

            // Assert
            Assert.Equal(string.Empty, staff.Password);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            DateTime expectedCreatedDate = new DateTime(2024, 3, 1);

            // Act
            staff.CreatedDate = expectedCreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, staff.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.ModifiedDate = null;

            // Assert
            Assert.Null(staff.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var staff = new OtherStaff();
            DateTime expectedModifiedDate = DateTime.Now;

            // Act
            staff.ModifiedDate = expectedModifiedDate;

            // Assert
            Assert.Equal(expectedModifiedDate, staff.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenTrue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.IsActive = true;

            // Assert
            Assert.True(staff.IsActive);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetCorrectly_WhenFalse()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.IsActive = false;

            // Assert
            Assert.False(staff.IsActive);
        }

        [Fact]
        public void OtherStaffProperties_ShouldSupportCompleteObjectInitialization()
        {
            // Arrange
            var expectedCreatedDate = new DateTime(2024, 1, 10);
            var expectedModifiedDate = new DateTime(2024, 2, 20);

            // Act
            var staff = new OtherStaff
            {
                StaffID = 25,
                Name = "Bob Martinez",
                Phone = "555-4444",
                Address = "321 Pine Road",
                Role = "Receptionist",
                Email = "bob.martinez@clinic.com",
                Password = "Password789",
                CreatedDate = expectedCreatedDate,
                ModifiedDate = expectedModifiedDate,
                IsActive = true
            };

            // Assert
            Assert.Equal(25, staff.StaffID);
            Assert.Equal("Bob Martinez", staff.Name);
            Assert.Equal("555-4444", staff.Phone);
            Assert.Equal("321 Pine Road", staff.Address);
            Assert.Equal("Receptionist", staff.Role);
            Assert.Equal("bob.martinez@clinic.com", staff.Email);
            Assert.Equal("Password789", staff.Password);
            Assert.Equal(expectedCreatedDate, staff.CreatedDate);
            Assert.Equal(expectedModifiedDate, staff.ModifiedDate);
            Assert.True(staff.IsActive);
        }

        [Fact]
        public void StaffID_ShouldAcceptZeroValue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.StaffID = 0;

            // Assert
            Assert.Equal(0, staff.StaffID);
        }

        [Fact]
        public void StaffID_ShouldAcceptNegativeValue()
        {
            // Arrange
            var staff = new OtherStaff();

            // Act
            staff.StaffID = -1;

            // Assert
            Assert.Equal(-1, staff.StaffID);
        }

        [Fact]
        public void Role_ShouldAcceptVariousRoleValues()
        {
            // Arrange
            var staff = new OtherStaff();
            string[] roles = { "Nurse", "Receptionist", "Technician", "Administrator" };

            foreach (var role in roles)
            {
                // Act
                staff.Role = role;

                // Assert
                Assert.Equal(role, staff.Role);
            }
        }

        [Fact]
        public void Phone_ShouldAcceptVariousPhoneFormats()
        {
            // Arrange
            var staff = new OtherStaff();
            string[] phoneNumbers = { "555-1234", "(555) 123-4567", "555.123.4567", "5551234567" };

            foreach (var phone in phoneNumbers)
            {
                // Act
                staff.Phone = phone;

                // Assert
                Assert.Equal(phone, staff.Phone);
            }
        }
    }
}
