using Xunit;
using ClinicManagement.Application.DTOs;

namespace ClinicManagement.Application.DTOs.Tests;

public class DepartmentDtoTests
{
    [Fact]
    public void DepartmentDto_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var dto = new DepartmentDto(1, "Cardiology", "Heart care department");

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Cardiology", dto.Name);
        Assert.Equal("Heart care department", dto.Description);
    }

    [Fact]
    public void DepartmentDto_WithEmptyValues_ShouldAcceptEmptyStrings()
    {
        // Arrange & Act
        var dto = new DepartmentDto(0, string.Empty, string.Empty);

        // Assert
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(string.Empty, dto.Description);
    }

    [Theory]
    [InlineData(1, "Neurology", "Brain department")]
    [InlineData(2, "Orthopedics", "Bone care")]
    [InlineData(3, "Pediatrics", "Children care")]
    public void DepartmentDto_WithVariousValues_ShouldInitializeCorrectly(int id, string name, string description)
    {
        // Arrange & Act
        var dto = new DepartmentDto(id, name, description);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(name, dto.Name);
        Assert.Equal(description, dto.Description);
    }

    [Fact]
    public void DepartmentDto_IsRecord_ShouldSupportEquality()
    {
        // Arrange
        var dto1 = new DepartmentDto(1, "Cardiology", "Heart care");
        var dto2 = new DepartmentDto(1, "Cardiology", "Heart care");

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void DepartmentDto_DifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var dto1 = new DepartmentDto(1, "Cardiology", "Heart care");
        var dto2 = new DepartmentDto(2, "Neurology", "Brain care");

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }
}
