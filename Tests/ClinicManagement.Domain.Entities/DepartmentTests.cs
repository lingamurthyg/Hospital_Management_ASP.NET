using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests;

public class DepartmentTests
{
    [Fact]
    public void Department_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var department = new Department();

        // Assert
        Assert.NotNull(department);
        Assert.Equal(string.Empty, department.Name);
        Assert.Equal(string.Empty, department.Description);
        Assert.NotNull(department.Doctors);
        Assert.Empty(department.Doctors);
    }

    [Fact]
    public void Department_SetName_ShouldUpdateValue()
    {
        // Arrange
        var department = new Department();

        // Act
        department.Name = "Cardiology";

        // Assert
        Assert.Equal("Cardiology", department.Name);
    }

    [Fact]
    public void Department_SetDescription_ShouldUpdateValue()
    {
        // Arrange
        var department = new Department();

        // Act
        department.Description = "Heart and cardiovascular care";

        // Assert
        Assert.Equal("Heart and cardiovascular care", department.Description);
    }

    [Fact]
    public void Department_Doctors_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var department = new Department();

        // Assert
        Assert.NotNull(department.Doctors);
        Assert.IsAssignableFrom<ICollection<Doctor>>(department.Doctors);
        Assert.Empty(department.Doctors);
    }

    [Fact]
    public void Department_Doctors_ShouldAllowAddingDoctors()
    {
        // Arrange
        var department = new Department();
        var doctor = new Doctor { Name = "Dr. Smith" };

        // Act
        department.Doctors.Add(doctor);

        // Assert
        Assert.Single(department.Doctors);
        Assert.Contains(doctor, department.Doctors);
    }

    [Fact]
    public void Department_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var department = new Department();

        // Act
        department.Name = "Neurology";
        department.Description = "Brain and nervous system treatment";

        // Assert
        Assert.Equal("Neurology", department.Name);
        Assert.Equal("Brain and nervous system treatment", department.Description);
    }

    [Fact]
    public void Department_EmptyName_ShouldBeAllowed()
    {
        // Arrange
        var department = new Department();

        // Act
        department.Name = string.Empty;

        // Assert
        Assert.Equal(string.Empty, department.Name);
    }

    [Fact]
    public void Department_EmptyDescription_ShouldBeAllowed()
    {
        // Arrange
        var department = new Department();

        // Act
        department.Description = string.Empty;

        // Assert
        Assert.Equal(string.Empty, department.Description);
    }
}
