using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClinicManagement.Domain.Entities;

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
        
        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Gender)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);
        
        builder.Property(p => p.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        // Relationships
        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Bills)
            .WithOne(b => b.Patient)
            .HasForeignKey(b => b.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.TreatmentHistories)
            .WithOne(t => t.Patient)
            .HasForeignKey(t => t.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
