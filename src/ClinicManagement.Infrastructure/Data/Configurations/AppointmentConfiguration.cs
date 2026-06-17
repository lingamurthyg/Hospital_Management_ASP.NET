using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(a => a.PatientID)
            .IsRequired();

        builder.Property(a => a.DoctorID)
            .IsRequired();

        builder.Property(a => a.TimeSlotID)
            .IsRequired();

        builder.Property(a => a.AppointmentDate)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(0);

        builder.Property(a => a.Disease)
            .HasMaxLength(200);

        builder.Property(a => a.Progress)
            .HasMaxLength(500);

        builder.Property(a => a.Prescription)
            .HasMaxLength(1000);

        builder.Property(a => a.IsPaid)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.FeedbackGiven)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(a => a.ModifiedDate);

        // Relationships
        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.TimeSlot)
            .WithMany(ts => ts.Appointments)
            .HasForeignKey(a => a.TimeSlotID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Bill)
            .WithOne(b => b.Appointment)
            .HasForeignKey<Bill>(b => b.AppointmentID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Feedback)
            .WithOne(f => f.Appointment)
            .HasForeignKey<Feedback>(f => f.AppointmentID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
