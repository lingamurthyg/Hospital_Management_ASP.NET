using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Data;

/// <summary>
/// Database context for Clinic Management System
/// </summary>
public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Staff> Staff => Set<Staff>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default schema to public for PostgreSQL
        modelBuilder.HasDefaultSchema("public");

        // Configure PostgreSQL specific settings
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);

        // Configure entity relationships and constraints
        ConfigurePatient(modelBuilder);
        ConfigureDoctor(modelBuilder);
        ConfigureDepartment(modelBuilder);
        ConfigureAppointment(modelBuilder);
        ConfigureStaff(modelBuilder);
        ConfigureFeedback(modelBuilder);
    }

    private void ConfigurePatient(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.CreatedBy).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(200);
            
            // Configure DateTime columns for PostgreSQL
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });
    }

    private void ConfigureDoctor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Qualification).HasMaxLength(500);
            
            // Configure numeric columns for PostgreSQL
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ChargesPerVisit).HasColumnType("decimal(18,2)");
            
            // Configure DateTime columns for PostgreSQL
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            
            // Configure relationship with Department
            entity.HasOne(d => d.Department)
                .WithMany(dept => dept.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureDepartment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CreatedBy).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(200);
            
            // Configure DateTime columns for PostgreSQL
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });
    }

    private void ConfigureAppointment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(50);
            
            // Configure DateTime columns for PostgreSQL
            entity.Property(e => e.AppointmentDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            
            entity.Property(e => e.TimeSlot).HasMaxLength(50);
            entity.Property(e => e.Disease).HasMaxLength(500);
            entity.Property(e => e.Progress).HasMaxLength(2000);
            entity.Property(e => e.Prescription).HasMaxLength(2000);
            entity.Property(e => e.BillAmount).HasColumnType("decimal(18,2)");
            
            // Configure relationships
            entity.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureStaff(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Designation).HasMaxLength(200);
            entity.Property(e => e.Qualification).HasMaxLength(500);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            
            // Configure DateTime columns for PostgreSQL
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });
    }

    private void ConfigureFeedback(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comments).HasMaxLength(2000);
            entity.Property(e => e.FeedbackDate).HasColumnType("timestamp without time zone");
            
            // Configure relationships
            entity.HasOne(f => f.Patient)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.Appointment)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Feedback>(f => f.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Doctor)
                .WithMany()
                .HasForeignKey(f => f.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatically set audit fields
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Patient || e.Entity is Department || e.Entity is Appointment || e.Entity is Staff)
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is Patient patient)
                {
                    patient.CreatedDate = DateTime.UtcNow;
                    patient.IsActive = true;
                }
                else if (entry.Entity is Department department)
                {
                    department.CreatedDate = DateTime.UtcNow;
                    department.IsActive = true;
                }
                else if (entry.Entity is Appointment appointment)
                {
                    appointment.CreatedDate = DateTime.UtcNow;
                    appointment.IsActive = true;
                }
                else if (entry.Entity is Staff staff)
                {
                    staff.CreatedDate = DateTime.UtcNow;
                    staff.IsActive = true;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is Patient patient)
                {
                    patient.ModifiedDate = DateTime.UtcNow;
                }
                else if (entry.Entity is Department department)
                {
                    department.ModifiedDate = DateTime.UtcNow;
                }
                else if (entry.Entity is Appointment appointment)
                {
                    appointment.ModifiedDate = DateTime.UtcNow;
                }
                else if (entry.Entity is Staff staff)
                {
                    staff.ModifiedDate = DateTime.UtcNow;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
