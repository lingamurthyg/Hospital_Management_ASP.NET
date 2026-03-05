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
        builder.Property(d => d.Specialization).IsRequired().HasMaxLength(100);
        builder.Property(d => d.ConsultationFee).HasColumnType("decimal(18,2)");
        builder.Property(d => d.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(d => d.IsActive).HasDefaultValue(true);

        builder.HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
