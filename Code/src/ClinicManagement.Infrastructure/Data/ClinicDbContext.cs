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

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default schema to public (PostgreSQL convention)
        modelBuilder.HasDefaultSchema("public");

        // Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);

        // Configure PostgreSQL specific settings
        ConfigurePostgreSqlSettings(modelBuilder);

        // Configure relationships
        ConfigureRelationships(modelBuilder);

        // Configure entity properties for PostgreSQL
        ConfigureEntityProperties(modelBuilder);
    }

    private void ConfigurePostgreSqlSettings(ModelBuilder modelBuilder)
    {
        // Enable PostgreSQL extensions if needed
        // modelBuilder.HasPostgresExtension("uuid-ossp");
        // modelBuilder.HasPostgresExtension("hstore");
    }

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(d => d.DeptNo)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Appointment)
            .WithOne(a => a.Bill)
            .HasForeignKey<Bill>(b => b.AppointmentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Patient)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Doctor)
            .WithMany(d => d.Feedbacks)
            .HasForeignKey(f => f.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureEntityProperties(ModelBuilder modelBuilder)
    {
        // Configure Patient entity
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configure Doctor entity
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ChargesPerVisit).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.Property(e => e.Specialization).HasMaxLength(200);
            entity.Property(e => e.ReputationIndex).HasColumnType("decimal(5,2)");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configure Department entity
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptNo);
            entity.Property(e => e.DeptName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        // Configure Appointment entity
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.AppointmentDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Timings).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Disease).HasMaxLength(200);
            entity.Property(e => e.Progress).HasMaxLength(1000);
            entity.Property(e => e.Prescription).HasMaxLength(2000);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });

        // Configure Slot entity
        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotID);
            entity.Property(e => e.SlotDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Timings).IsRequired().HasMaxLength(50);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });

        // Configure Bill entity
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillID);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.BillDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
        });

        // Configure Feedback entity
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackID);
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comments).HasMaxLength(1000);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Configure Staff entity
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        // Configure Admin entity
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        // Configure DateTime to use timestamp without time zone for PostgreSQL
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");
        
        configurationBuilder.Properties<DateTime?>()
            .HaveColumnType("timestamp without time zone");
    }
}
