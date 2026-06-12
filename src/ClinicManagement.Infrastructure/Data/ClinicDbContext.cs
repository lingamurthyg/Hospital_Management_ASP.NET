using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data;

/// <summary>
/// Database context for the Clinic Management System
/// </summary>
public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<TreatmentHistory> TreatmentHistories { get; set; }
    public DbSet<OtherStaff> OtherStaffs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
    }
}
