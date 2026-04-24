using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Email).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Password).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Specialization).HasMaxLength(100);
        builder.Property(d => d.PhoneNumber).HasMaxLength(20);
        builder.Property(d => d.Address).HasMaxLength(500);
        builder.Property(d => d.IsActive).HasDefaultValue(true);

        builder.HasIndex(d => d.Email).IsUnique();
    }
}
