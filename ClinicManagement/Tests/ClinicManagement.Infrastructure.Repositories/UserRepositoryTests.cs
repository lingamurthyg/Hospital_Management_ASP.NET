using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ClinicManagement.Infrastructure.Repositories;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Tests.Infrastructure.Repositories;

public class UserRepositoryTests
{
    private readonly DbContextOptions<ClinicDbContext> _options;
    private readonly Mock<ILogger<UserRepository>> _mockLogger;

    public UserRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _mockLogger = new Mock<ILogger<UserRepository>>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsActiveUsers()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        context.Users.AddRange(
            new User { Id = 1, Name = "User1", Email = "user1@test.com", Password = "pass1", PhoneNo = "1111", Gender = "M", IsActive = true, CreatedBy = "System" },
            new User { Id = 2, Name = "User2", Email = "user2@test.com", Password = "pass2", PhoneNo = "2222", Gender = "F", IsActive = true, CreatedBy = "System" },
            new User { Id = 3, Name = "User3", Email = "user3@test.com", Password = "pass3", PhoneNo = "3333", Gender = "M", IsActive = false, CreatedBy = "System" }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUserById()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenUserNotActive()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = false, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsUserByEmail()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByEmailAsync("test@test.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task AddAsync_AddsUserSuccessfully()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User
        {
            Name = "New User",
            Email = "newuser@test.com",
            Password = "password",
            PhoneNo = "5555",
            Gender = "F",
            IsActive = true,
            CreatedBy = "System",
            CreatedDate = DateTime.UtcNow
        };

        // Act
        var result = await repository.AddAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("New User", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesUserSuccessfully()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Original Name", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        user.Name = "Updated Name";
        await repository.UpdateAsync(user);

        // Assert
        var updatedUser = await context.Users.FindAsync(1);
        Assert.NotNull(updatedUser);
        Assert.Equal("Updated Name", updatedUser.Name);
    }

    [Fact]
    public async Task DeleteAsync_SetsIsActiveToFalse()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(1);

        // Assert
        var deletedUser = await context.Users.FindAsync(1);
        Assert.NotNull(deletedUser);
        Assert.False(deletedUser.IsActive);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenUserExists()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task EmailExistsAsync_ReturnsTrue_WhenEmailExists()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "pass", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.EmailExistsAsync("test@test.com");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task EmailExistsAsync_ReturnsFalse_WhenEmailDoesNotExist()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.EmailExistsAsync("nonexistent@test.com");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateLoginAsync_ReturnsUser_WithValidCredentials()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "password123", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ValidateLoginAsync("test@test.com", "password123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task ValidateLoginAsync_ReturnsNull_WithInvalidCredentials()
    {
        // Arrange
        using var context = new ClinicDbContext(_options);
        var repository = new UserRepository(context, _mockLogger.Object);

        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Password = "password123", PhoneNo = "1234", Gender = "M", IsActive = true, CreatedBy = "System" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ValidateLoginAsync("test@test.com", "wrongpassword");

        // Assert
        Assert.Null(result);
    }
}
