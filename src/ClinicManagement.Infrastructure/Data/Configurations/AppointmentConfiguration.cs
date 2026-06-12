using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Appointment
/// </summary>
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointment");
        
        builder.HasKey(a => a.AppointmentID);
        
        builder.Property(a => a.Timings)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(a => a.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("Pending");
        
        builder.Property(a => a.Disease)
            .HasMaxLength(100);
        
        builder.Property(a => a.Progress)
            .HasMaxLength(500);
        
        builder.Property(a => a.Prescription)
            .HasMaxLength(500);
        
        builder.Property(a => a.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        // Relationships
        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Bill)
            .WithOne(b => b.Appointment)
            .HasForeignKey<Bill>(b => b.AppointmentID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
