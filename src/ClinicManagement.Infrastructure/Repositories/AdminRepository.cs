using ClinicManagement.Domain.Entities;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ClinicDbContext _context;

    public AdminRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id && a.IsActive, cancellationToken);
    }

    public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email && a.IsActive, cancellationToken);
    }

    public async Task<Admin?> ValidateLoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        return await _context.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Email == email && a.Password == password && a.IsActive, cancellationToken);
    }
}
