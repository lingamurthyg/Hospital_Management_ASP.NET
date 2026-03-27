using Xunit;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Domain.Entities.Tests;

public class ConcreteEntity : BaseEntity { }

public class BaseEntityTests
{
    [Fact]
    public void BaseEntity_Constructor_ShouldInitializeDefaultValues()
    {
        // Arrange & Act
        var entity = new ConcreteEntity();

        // Assert
        Assert.Equal(0, entity.Id);
        Assert.True(entity.IsActive);
        Assert.Equal(string.Empty, entity.CreatedBy);
        Assert.Null(entity.ModifiedBy);
        Assert.Null(entity.ModifiedDate);
    }

    [Fact]
    public void BaseEntity_CreatedDate_ShouldBeSetToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var entity = new ConcreteEntity();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.InRange(entity.CreatedDate, beforeCreation, afterCreation);
    }

    [Fact]
    public void BaseEntity_SetId_ShouldUpdateValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.Id = 123;

        // Assert
        Assert.Equal(123, entity.Id);
    }

    [Fact]
    public void BaseEntity_SetIsActive_ShouldUpdateValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.IsActive = false;

        // Assert
        Assert.False(entity.IsActive);
    }

    [Fact]
    public void BaseEntity_SetCreatedBy_ShouldUpdateValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", entity.CreatedBy);
    }

    [Fact]
    public void BaseEntity_SetModifiedBy_ShouldUpdateValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.ModifiedBy = "user1";

        // Assert
        Assert.Equal("user1", entity.ModifiedBy);
    }

    [Fact]
    public void BaseEntity_SetModifiedDate_ShouldUpdateValue()
    {
        // Arrange
        var entity = new ConcreteEntity();
        var modifiedDate = DateTime.UtcNow;

        // Act
        entity.ModifiedDate = modifiedDate;

        // Assert
        Assert.Equal(modifiedDate, entity.ModifiedDate);
    }

    [Fact]
    public void BaseEntity_SetAllProperties_ShouldUpdateValues()
    {
        // Arrange
        var entity = new ConcreteEntity();
        var createdDate = DateTime.UtcNow.AddDays(-5);
        var modifiedDate = DateTime.UtcNow.AddDays(-1);

        // Act
        entity.Id = 456;
        entity.CreatedDate = createdDate;
        entity.ModifiedDate = modifiedDate;
        entity.IsActive = false;
        entity.CreatedBy = "system";
        entity.ModifiedBy = "admin";

        // Assert
        Assert.Equal(456, entity.Id);
        Assert.Equal(createdDate, entity.CreatedDate);
        Assert.Equal(modifiedDate, entity.ModifiedDate);
        Assert.False(entity.IsActive);
        Assert.Equal("system", entity.CreatedBy);
        Assert.Equal("admin", entity.ModifiedBy);
    }

    [Fact]
    public void BaseEntity_ModifiedBy_ShouldAcceptNull()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.ModifiedBy = null;

        // Assert
        Assert.Null(entity.ModifiedBy);
    }

    [Fact]
    public void BaseEntity_ModifiedDate_ShouldAcceptNull()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.ModifiedDate = null;

        // Assert
        Assert.Null(entity.ModifiedDate);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void BaseEntity_IsActive_ShouldAcceptBooleanValues(bool isActive)
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        entity.IsActive = isActive;

        // Assert
        Assert.Equal(isActive, entity.IsActive);
    }
}
