using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for Doctor
/// </summary>
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");

        builder.HasKey(d => d.DoctorID);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(d => d.Email)
            .IsUnique();

        builder.Property(d => d.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(d => d.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(d => d.BirthDate)
            .IsRequired();

        builder.Property(d => d.Gender)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(d => d.Age)
            .IsRequired();

        builder.Property(d => d.DeptNo)
            .IsRequired();

        builder.Property(d => d.Experience)
            .IsRequired();

        builder.Property(d => d.Salary)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.ChargesPerVisit)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.Qualification)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Specialization)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.ReputationIndex)
            .IsRequired()
            .HasDefaultValue(0f);

        builder.Property(d => d.PatientsTreated)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.Status)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(d => d.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(d => d.ModifiedDate);

        // Relationships
        builder.HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(d => d.DeptNo)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.TimeSlots)
            .WithOne(ts => ts.Doctor)
            .HasForeignKey(ts => ts.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
