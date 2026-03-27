using Xunit;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities.Tests;

public class DoctorTests
{
    [Fact]
    public void Doctor_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var doctor = new Doctor();

        // Assert
        Assert.NotNull(doctor);
        Assert.Equal(string.Empty, doctor.Name);
        Assert.Equal(string.Empty, doctor.Email);
        Assert.Equal(string.Empty, doctor.PasswordHash);
        Assert.Equal(string.Empty, doctor.Phone);
        Assert.Equal(string.Empty, doctor.Address);
        Assert.Equal(string.Empty, doctor.Specialization);
        Assert.Equal(string.Empty, doctor.Qualification);
        Assert.NotNull(doctor.Appointments);
        Assert.NotNull(doctor.TimeSlots);
        Assert.Empty(doctor.Appointments);
        Assert.Empty(doctor.TimeSlots);
    }

    [Fact]
    public void Doctor_Age_ShouldCalculateCorrectly()
    {
        // Arrange
        var doctor = new Doctor
        {
            BirthDate = new DateTime(1990, 1, 1)
        };

        // Act
        var age = doctor.Age;

        // Assert
        Assert.Equal(DateTime.Now.Year - 1990, age);
    }

    [Fact]
    public void Doctor_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var doctor = new Doctor();
        var birthDate = new DateTime(1985, 5, 15);

        // Act
        doctor.Name = "Dr. John Smith";
        doctor.Email = "john.smith@clinic.com";
        doctor.PasswordHash = "hashedpassword";
        doctor.Phone = "1234567890";
        doctor.Address = "123 Main St";
        doctor.BirthDate = birthDate;
        doctor.Gender = Gender.Male;
        doctor.DepartmentId = 1;
        doctor.WorkExperience = 10;
        doctor.Salary = 80000m;
        doctor.ChargesPerVisit = 150m;
        doctor.Specialization = "Cardiology";
        doctor.Qualification = "MD";
        doctor.ReputationIndex = 4.5f;
        doctor.PatientsTreated = 500;

        // Assert
        Assert.Equal("Dr. John Smith", doctor.Name);
        Assert.Equal("john.smith@clinic.com", doctor.Email);
        Assert.Equal("hashedpassword", doctor.PasswordHash);
        Assert.Equal("1234567890", doctor.Phone);
        Assert.Equal("123 Main St", doctor.Address);
        Assert.Equal(birthDate, doctor.BirthDate);
        Assert.Equal(Gender.Male, doctor.Gender);
        Assert.Equal(1, doctor.DepartmentId);
        Assert.Equal(10, doctor.WorkExperience);
        Assert.Equal(80000m, doctor.Salary);
        Assert.Equal(150m, doctor.ChargesPerVisit);
        Assert.Equal("Cardiology", doctor.Specialization);
        Assert.Equal("MD", doctor.Qualification);
        Assert.Equal(4.5f, doctor.ReputationIndex);
        Assert.Equal(500, doctor.PatientsTreated);
    }

    [Fact]
    public void Doctor_Age_WithCurrentYearBirthDate_ShouldReturnZero()
    {
        // Arrange
        var doctor = new Doctor
        {
            BirthDate = new DateTime(DateTime.Now.Year, 1, 1)
        };

        // Act
        var age = doctor.Age;

        // Assert
        Assert.Equal(0, age);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void Doctor_Gender_ShouldAcceptAllValidValues(Gender gender)
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.Gender = gender;

        // Assert
        Assert.Equal(gender, doctor.Gender);
    }

    [Fact]
    public void Doctor_Department_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.Department = null;

        // Assert
        Assert.Null(doctor.Department);
    }

    [Fact]
    public void Doctor_Appointments_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var doctor = new Doctor();

        // Assert
        Assert.NotNull(doctor.Appointments);
        Assert.IsAssignableFrom<ICollection<Appointment>>(doctor.Appointments);
        Assert.Empty(doctor.Appointments);
    }

    [Fact]
    public void Doctor_TimeSlots_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var doctor = new Doctor();

        // Assert
        Assert.NotNull(doctor.TimeSlots);
        Assert.IsAssignableFrom<ICollection<TimeSlot>>(doctor.TimeSlots);
        Assert.Empty(doctor.TimeSlots);
    }

    [Fact]
    public void Doctor_ChargesPerVisit_ShouldAcceptDecimalValues()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.ChargesPerVisit = 199.99m;

        // Assert
        Assert.Equal(199.99m, doctor.ChargesPerVisit);
    }

    [Fact]
    public void Doctor_ReputationIndex_ShouldAcceptFloatValues()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.ReputationIndex = 4.75f;

        // Assert
        Assert.Equal(4.75f, doctor.ReputationIndex);
    }
}
