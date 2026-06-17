using Microsoft.EntityFrameworkCore;
using ClinicManagement.Domain.Entities;

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
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<TreatmentHistory> TreatmentHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);

        // Configure relationships
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(d => d.Department)
                .WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DeptNo)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptNo);
            entity.Property(e => e.DeptName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffID);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Qualification).HasMaxLength(200);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.Disease).HasMaxLength(200);
            entity.Property(e => e.Progress).HasMaxLength(500);
            entity.Property(e => e.Prescription).HasMaxLength(1000);

            entity.HasOne(d => d.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.TimeSlot)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TimeSlotID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotID);

            entity.HasOne(d => d.Doctor)
                .WithMany(p => p.TimeSlots)
                .HasForeignKey(d => d.DoctorID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillID);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.Patient)
                .WithMany(p => p.Bills)
                .HasForeignKey(d => d.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Appointment)
                .WithOne(p => p.Bill)
                .HasForeignKey<Bill>(d => d.AppointmentID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TreatmentHistory>(entity =>
        {
            entity.HasKey(e => e.TreatmentID);
            entity.Property(e => e.Disease).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Treatment).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Prescription).HasMaxLength(1000);
            entity.Property(e => e.Progress).HasMaxLength(500);

            entity.HasOne(d => d.Patient)
                .WithMany(p => p.TreatmentHistories)
                .HasForeignKey(d => d.PatientID)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
