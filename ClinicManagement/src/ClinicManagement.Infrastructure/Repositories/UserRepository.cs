using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ClinicDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ClinicDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            throw;
        }
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .Where(u => u.Id == id && u.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user with ID {UserId}", id);
            throw;
        }
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .Where(u => u.Email == email && u.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user with email {Email}", email);
            throw;
        }
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User {UserId} created successfully", user.Id);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            throw;
        }
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User {UserId} updated successfully", user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", user.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await GetByIdAsync(id, cancellationToken);
            if (user != null)
            {
                user.IsActive = false;
                await UpdateAsync(user, cancellationToken);
                _logger.LogInformation("User {UserId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users.AnyAsync(u => u.Id == id && u.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {UserId} exists", id);
            throw;
        }
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if email {Email} exists", email);
            throw;
        }
    }

    public async Task<User?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .Where(u => u.Email == email && u.Password == password && u.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login for email {Email}", email);
            throw;
        }
    }
}
