using Xunit;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities.Tests;

public class PatientTests
{
    [Fact]
    public void Patient_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var patient = new Patient();

        // Assert
        Assert.NotNull(patient);
        Assert.Equal(string.Empty, patient.Name);
        Assert.Equal(string.Empty, patient.Email);
        Assert.Equal(string.Empty, patient.PasswordHash);
        Assert.Equal(string.Empty, patient.Phone);
        Assert.Equal(string.Empty, patient.Address);
        Assert.NotNull(patient.Appointments);
        Assert.NotNull(patient.Bills);
        Assert.NotNull(patient.Feedbacks);
        Assert.Empty(patient.Appointments);
        Assert.Empty(patient.Bills);
        Assert.Empty(patient.Feedbacks);
    }

    [Fact]
    public void Patient_Age_ShouldCalculateCorrectly()
    {
        // Arrange
        var patient = new Patient
        {
            BirthDate = new DateTime(1995, 3, 10)
        };

        // Act
        var age = patient.Age;

        // Assert
        Assert.Equal(DateTime.Now.Year - 1995, age);
    }

    [Fact]
    public void Patient_SetName_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Name = "Jane Doe";

        // Assert
        Assert.Equal("Jane Doe", patient.Name);
    }

    [Fact]
    public void Patient_SetEmail_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Email = "jane.doe@example.com";

        // Assert
        Assert.Equal("jane.doe@example.com", patient.Email);
    }

    [Fact]
    public void Patient_SetPasswordHash_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.PasswordHash = "hashedpassword123";

        // Assert
        Assert.Equal("hashedpassword123", patient.PasswordHash);
    }

    [Fact]
    public void Patient_SetPhone_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Phone = "9876543210";

        // Assert
        Assert.Equal("9876543210", patient.Phone);
    }

    [Fact]
    public void Patient_SetAddress_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Address = "456 Oak Avenue";

        // Assert
        Assert.Equal("456 Oak Avenue", patient.Address);
    }

    [Fact]
    public void Patient_SetBirthDate_ShouldUpdateValue()
    {
        // Arrange
        var patient = new Patient();
        var birthDate = new DateTime(2000, 7, 20);

        // Act
        patient.BirthDate = birthDate;

        // Assert
        Assert.Equal(birthDate, patient.BirthDate);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void Patient_SetGender_ShouldUpdateValue(Gender gender)
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.Gender = gender;

        // Assert
        Assert.Equal(gender, patient.Gender);
    }

    [Fact]
    public void Patient_Age_WithCurrentYearBirthDate_ShouldReturnZero()
    {
        // Arrange
        var patient = new Patient
        {
            BirthDate = new DateTime(DateTime.Now.Year, 6, 1)
        };

        // Act
        var age = patient.Age;

        // Assert
        Assert.Equal(0, age);
    }

    [Fact]
    public void Patient_Appointments_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var patient = new Patient();

        // Assert
        Assert.NotNull(patient.Appointments);
        Assert.IsAssignableFrom<ICollection<Appointment>>(patient.Appointments);
        Assert.Empty(patient.Appointments);
    }

    [Fact]
    public void Patient_Bills_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var patient = new Patient();

        // Assert
        Assert.NotNull(patient.Bills);
        Assert.IsAssignableFrom<ICollection<Bill>>(patient.Bills);
        Assert.Empty(patient.Bills);
    }

    [Fact]
    public void Patient_Feedbacks_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var patient = new Patient();

        // Assert
        Assert.NotNull(patient.Feedbacks);
        Assert.IsAssignableFrom<ICollection<Feedback>>(patient.Feedbacks);
        Assert.Empty(patient.Feedbacks);
    }

    [Fact]
    public void Patient_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var patient = new Patient();
        var birthDate = new DateTime(1988, 11, 25);

        // Act
        patient.Name = "John Smith";
        patient.Email = "john@example.com";
        patient.PasswordHash = "secureHash";
        patient.Phone = "1231231234";
        patient.Address = "789 Pine Street";
        patient.BirthDate = birthDate;
        patient.Gender = Gender.Male;

        // Assert
        Assert.Equal("John Smith", patient.Name);
        Assert.Equal("john@example.com", patient.Email);
        Assert.Equal("secureHash", patient.PasswordHash);
        Assert.Equal("1231231234", patient.Phone);
        Assert.Equal("789 Pine Street", patient.Address);
        Assert.Equal(birthDate, patient.BirthDate);
        Assert.Equal(Gender.Male, patient.Gender);
    }
}
