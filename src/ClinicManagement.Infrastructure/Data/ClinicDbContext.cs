using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<TreatmentHistory> TreatmentHistories { get; set; }
    public DbSet<OtherStaff> OtherStaffs { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);

        // Additional configurations if needed
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
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
            entity.Property(e => e.DeptName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
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
            entity.Property(e => e.Timings).IsRequired().HasMaxLength(50);
            
            entity.HasOne(d => d.Doctor)
                .WithMany(p => p.TimeSlots)
                .HasForeignKey(d => d.DoctorID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillID);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            
            entity.HasOne(d => d.Appointment)
                .WithOne(p => p.Bill)
                .HasForeignKey<Bill>(d => d.AppointmentID)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(d => d.Patient)
                .WithMany(p => p.Bills)
                .HasForeignKey(d => d.PatientID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TreatmentHistory>(entity =>
        {
            entity.HasKey(e => e.TreatmentID);
            entity.Property(e => e.Disease).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Treatment).HasMaxLength(500);
            entity.Property(e => e.Prescription).HasMaxLength(1000);
            
            entity.HasOne(d => d.Patient)
                .WithMany(p => p.TreatmentHistories)
                .HasForeignKey(d => d.PatientID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OtherStaff>(entity =>
        {
            entity.HasKey(e => e.StaffID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Qualification).HasMaxLength(200);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminID);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
