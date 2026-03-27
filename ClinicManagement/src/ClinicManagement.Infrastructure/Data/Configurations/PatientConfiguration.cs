using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("PatientID");

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Phone).IsRequired().HasMaxLength(15);
        builder.Property(p => p.Address).IsRequired().HasMaxLength(200);
        builder.Property(p => p.BirthDate).IsRequired();
        builder.Property(p => p.Gender).IsRequired().HasConversion<int>();
        builder.Property(p => p.IsActive).HasDefaultValue(true).HasColumnName("Status");
        builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Bills)
            .WithOne(b => b.Patient)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Feedbacks)
            .WithOne(f => f.Patient)
            .HasForeignKey(f => f.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
