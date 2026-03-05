using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(b => b.Status).IsRequired().HasMaxLength(50);
        builder.Property(b => b.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(b => b.IsActive).HasDefaultValue(true);

        builder.HasOne(b => b.Patient)
            .WithMany(p => p.Bills)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
