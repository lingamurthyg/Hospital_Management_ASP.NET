using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(e => e.PatientID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(100).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");
            entity.HasKey(e => e.DoctorID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ChargesPerVisit).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DeptNo);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");
            entity.HasKey(e => e.DeptNo);
            entity.Property(e => e.DeptName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("Appointment");
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Disease).HasMaxLength(200);
            entity.Property(e => e.Progress).HasMaxLength(500);
            entity.Property(e => e.Prescription).HasMaxLength(500);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientID);

            entity.HasOne(d => d.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorID);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("OtherStaff");
            entity.HasKey(e => e.StaffID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });
    }
}
