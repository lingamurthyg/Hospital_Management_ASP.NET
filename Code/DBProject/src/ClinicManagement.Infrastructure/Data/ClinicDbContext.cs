using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Data
{
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
        public DbSet<FreeSlot> FreeSlots { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<TreatmentHistory> TreatmentHistories { get; set; }
        public DbSet<OtherStaff> OtherStaff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set default schema to public for PostgreSQL
            modelBuilder.HasDefaultSchema("public");

            // Configure PostgreSQL extensions if needed
            // modelBuilder.HasPostgresExtension("uuid-ossp");

            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(30);
                entity.HasIndex(e => e.Email).IsUnique();
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp without time zone");
            });

            // Configure Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(30);
                entity.HasIndex(e => e.Email).IsUnique();
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp without time zone");
                
                entity.HasOne(d => d.Department)
                    .WithMany(dept => dept.Doctors)
                    .HasForeignKey(d => d.DeptNo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Department entity
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptNo);
                entity.Property(e => e.DeptName).IsRequired().HasMaxLength(30);
            });

            // Configure Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentID);
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("timestamp without time zone");
                
                entity.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.FreeSlot)
                    .WithMany(fs => fs.Appointments)
                    .HasForeignKey(a => a.FreeSlotID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure FreeSlot entity
            modelBuilder.Entity<FreeSlot>(entity =>
            {
                entity.HasKey(e => e.FreeSlotID);
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.SlotDate)
                    .HasColumnType("timestamp without time zone");
                
                entity.HasOne(fs => fs.Doctor)
                    .WithMany(d => d.FreeSlots)
                    .HasForeignKey(fs => fs.DoctorID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Bill entity
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.BillID);
                
                // PostgreSQL uses numeric type for decimal
                entity.Property(e => e.Amount)
                    .HasColumnType("numeric(18,2)");
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.BillDate)
                    .HasColumnType("timestamp without time zone");
                
                entity.HasOne(b => b.Appointment)
                    .WithOne(a => a.Bill)
                    .HasForeignKey<Bill>(b => b.AppointmentID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Patient)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(b => b.PatientID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure TreatmentHistory entity
            modelBuilder.Entity<TreatmentHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryID);
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.TreatmentDate)
                    .HasColumnType("timestamp without time zone");
                
                entity.HasOne(th => th.Patient)
                    .WithMany(p => p.TreatmentHistories)
                    .HasForeignKey(th => th.PatientID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure OtherStaff entity
            modelBuilder.Entity<OtherStaff>(entity =>
            {
                entity.HasKey(e => e.StaffID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
                
                // PostgreSQL-specific: Configure timestamp without timezone
                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp without time zone");
            });
        }
    }
}
