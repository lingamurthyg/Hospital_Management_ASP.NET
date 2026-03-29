using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        builder.HasKey(d => d.DoctorID);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Phone)
            .HasMaxLength(20);

        builder.Property(d => d.Address)
            .HasMaxLength(500);

        builder.Property(d => d.Specialization)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Password)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.CreatedDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(d => d.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(d => d.Email)
            .IsUnique();

        builder.HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.TimeSlots)
            .WithOne(ts => ts.Doctor)
            .HasForeignKey(ts => ts.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
