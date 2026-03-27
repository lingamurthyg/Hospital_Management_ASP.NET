using Xunit;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities.Tests;

public class StaffTests
{
    [Fact]
    public void Staff_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var staff = new Staff();

        // Assert
        Assert.NotNull(staff);
        Assert.Equal(string.Empty, staff.Name);
        Assert.Equal(string.Empty, staff.Phone);
        Assert.Equal(string.Empty, staff.Address);
        Assert.Equal(string.Empty, staff.Designation);
        Assert.Equal(string.Empty, staff.Qualification);
    }

    [Fact]
    public void Staff_SetName_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Name = "Alice Johnson";

        // Assert
        Assert.Equal("Alice Johnson", staff.Name);
    }

    [Fact]
    public void Staff_SetPhone_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Phone = "5551234567";

        // Assert
        Assert.Equal("5551234567", staff.Phone);
    }

    [Fact]
    public void Staff_SetAddress_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Address = "321 Elm Street";

        // Assert
        Assert.Equal("321 Elm Street", staff.Address);
    }

    [Fact]
    public void Staff_SetBirthDate_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();
        var birthDate = new DateTime(1992, 8, 15);

        // Act
        staff.BirthDate = birthDate;

        // Assert
        Assert.Equal(birthDate, staff.BirthDate);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public void Staff_SetGender_ShouldUpdateValue(Gender gender)
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Gender = gender;

        // Assert
        Assert.Equal(gender, staff.Gender);
    }

    [Fact]
    public void Staff_SetDesignation_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Designation = "Receptionist";

        // Assert
        Assert.Equal("Receptionist", staff.Designation);
    }

    [Fact]
    public void Staff_SetSalary_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Salary = 45000m;

        // Assert
        Assert.Equal(45000m, staff.Salary);
    }

    [Fact]
    public void Staff_SetQualification_ShouldUpdateValue()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Qualification = "Bachelor's Degree";

        // Assert
        Assert.Equal("Bachelor's Degree", staff.Qualification);
    }

    [Fact]
    public void Staff_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var staff = new Staff();
        var birthDate = new DateTime(1990, 4, 20);

        // Act
        staff.Name = "Bob Williams";
        staff.Phone = "5559876543";
        staff.Address = "654 Maple Drive";
        staff.BirthDate = birthDate;
        staff.Gender = Gender.Male;
        staff.Designation = "Nurse";
        staff.Salary = 55000m;
        staff.Qualification = "RN";

        // Assert
        Assert.Equal("Bob Williams", staff.Name);
        Assert.Equal("5559876543", staff.Phone);
        Assert.Equal("654 Maple Drive", staff.Address);
        Assert.Equal(birthDate, staff.BirthDate);
        Assert.Equal(Gender.Male, staff.Gender);
        Assert.Equal("Nurse", staff.Designation);
        Assert.Equal(55000m, staff.Salary);
        Assert.Equal("RN", staff.Qualification);
    }

    [Fact]
    public void Staff_Salary_ShouldAcceptDecimalValues()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Salary = 48500.75m;

        // Assert
        Assert.Equal(48500.75m, staff.Salary);
    }

    [Fact]
    public void Staff_Salary_ShouldAcceptZero()
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Salary = 0m;

        // Assert
        Assert.Equal(0m, staff.Salary);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Tech")]
    [InlineData("Administrator")]
    public void Staff_Designation_ShouldAcceptVariousValues(string designation)
    {
        // Arrange
        var staff = new Staff();

        // Act
        staff.Designation = designation;

        // Assert
        Assert.Equal(designation, staff.Designation);
    }
}
