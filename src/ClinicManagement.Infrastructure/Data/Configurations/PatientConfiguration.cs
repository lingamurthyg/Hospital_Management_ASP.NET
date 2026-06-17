using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Patient
/// </summary>
public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(p => p.PatientID);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Age)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.ModifiedDate);

        // Relationships
        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Bills)
            .WithOne(b => b.Patient)
            .HasForeignKey(b => b.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Feedbacks)
            .WithOne(f => f.Patient)
            .HasForeignKey(f => f.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
